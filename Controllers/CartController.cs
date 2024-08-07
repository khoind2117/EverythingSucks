using EverythingSucks.Data;
using EverythingSucks.Models;
using EverythingSucks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using EverythingSucks.Helpers;
using System.Security.Claims;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.CodeAnalysis;
using System.Drawing;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using EverythingSucks.Services;

namespace EverythingSucks.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly PaypalClient _paypalClient;
        private readonly IExchangeRateProvider _exchangeRateProvider;
        private readonly IVnPayService _vnPayService;

        public CartController(ApplicationDbContext context,
            UserManager<User> userManager,
            PaypalClient paypalClient,
            IExchangeRateProvider exchangeRateProvider,
            IVnPayService vnPayService)
        {
            _context = context;
            _userManager = userManager;
            _paypalClient = paypalClient;
            _exchangeRateProvider = exchangeRateProvider;
            _vnPayService = vnPayService;
        }

        public const string CART_SESSION_KEY = "MYCART";

        private Cart GetCartFromSession()
        {
            var cartJson = HttpContext.Session.GetString(CART_SESSION_KEY);
            return cartJson == null ? new Cart() : System.Text.Json.JsonSerializer.Deserialize<Cart>(cartJson, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
        }


        private void SaveCartToSession(Cart cart)
        {
            var cartJson = System.Text.Json.JsonSerializer.Serialize(cart, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            HttpContext.Session.SetString(CART_SESSION_KEY, cartJson);
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.ProductColor)
                            .ThenInclude(pc => pc.Product)
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.ProductColor)
                            .ThenInclude(pc => pc.ProductImages)
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Size)
                    .SingleOrDefaultAsync(c => c.UserId == userId);


                return View(cart); // Truyền toàn bộ đối tượng Cart ra View
            }
            else
            {
                var cart = GetCartFromSession();
                return View(cart); // Truyền toàn bộ đối tượng Cart ra View
            }
        }

        public async Task<IActionResult> AddToCart(Guid productId, Guid colorId, Guid sizeId, int quantity = 1)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var cart = await _context.Carts
                    .SingleOrDefaultAsync(c => c.UserId == userId);


                var productColor = await _context.ProductColors
                    .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.ColorId == colorId);

                var size = await _context.Sizes
                   .FirstOrDefaultAsync(s => s.Id == sizeId);

                var existingItem = await _context.CartItems
                    .SingleOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ProductColorId == productColor.Id && ci.SizeId == size.Id);

                if (existingItem == null)
                {
                    var newItem = new CartItem
                    {
                        ProductColorId = productColor.Id,
                        ProductColor = productColor,
                        Quantity = quantity,
                        SizeId = size.Id,
                        Size = size,
                        CartId = cart.Id,
                        Cart = cart
                    };

                    // cart.CartItems.Add(newItem);
                    // Gây ra lỗi concurrency, khi đang truy vấn Cart mà lại Add thẳng vào Entity đang được Track (vừa Track, vừa Add thẳng vào Entity) => thay vào đó Add vào DB
                    // Lỗi: The database operation was expected to affect 1 row(s) but actually affected 0 row(s) data may have been modified or deleted
                    // Khi sử dụng Entity Framework, nếu thêm một thực thể vào một DbContext đang theo dõi các thay đổi của nó,
                    // điều này có thể dẫn đến lỗi đồng bộ(concurrency) nếu có sự thay đổi bất ngờ trong cơ sở dữ liệu.

                    // Thêm trực tiếp vào DB
                    _context.CartItems.Add(newItem);
                }
                else
                {
                    existingItem.Quantity += quantity;
                    _context.Entry(existingItem).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                var cart = GetCartFromSession();

                var productColor = await _context.ProductColors
                    .Include(pc => pc.Product)
                    .Include(pc => pc.ProductImages)
                    .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.ColorId == colorId);

                var size = await _context.Sizes
                    .FirstOrDefaultAsync(s => s.Id == sizeId);

                var existingItem = cart.CartItems
                        .SingleOrDefault(ci => ci.ProductColor?.ProductId == productId && ci.ProductColor?.ColorId == colorId && ci.SizeId == sizeId);

                if (existingItem == null)
                {
                    var newItem = new CartItem
                    {
                        ProductColorId = productColor.Id,
                        ProductColor = productColor,
                        Quantity = quantity,
                        SizeId = size.Id,
                        Size = size,
                    };

                    cart.CartItems.Add(newItem);
                }
                else
                {
                    existingItem.Quantity += quantity;
                }

                SaveCartToSession(cart);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart([FromBody] List<CartItemUpdateViewModel> cartItems)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .SingleOrDefaultAsync(c => c.UserId == userId);

                foreach (var item in cartItems)
                {
                    var productColor = await _context.ProductColors
                        .FirstOrDefaultAsync(pc => pc.ProductId == item.ProductId && pc.ColorId == item.ColorId);

                    var size = await _context.Sizes
                        .FirstOrDefaultAsync(s => s.Id == item.SizeId);

                    var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductColorId == productColor.Id && ci.SizeId == size.Id);
                    if (cartItem != null)
                    {
                        cartItem.Quantity = item.Quantity;
                        _context.Entry(cartItem).State = EntityState.Modified;
                    }
                }

                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            else
            {
                var cart = GetCartFromSession();

                foreach (var item in cartItems)
                {
                    var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductColor.ProductId == item.ProductId && ci.ProductColor.ColorId == item.ColorId && ci.SizeId == item.SizeId);
                    if (cartItem != null)
                    {
                        cartItem.Quantity = item.Quantity;
                    }
                }

                SaveCartToSession(cart);

                return Json(new { success = true });
            }
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromCart(Guid productId, Guid colorId, Guid sizeId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .SingleOrDefaultAsync(c => c.UserId == userId);

                var productColor = await _context.ProductColors
                    .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.ColorId == colorId);

                var itemToRemove = cart.CartItems
                    .FirstOrDefault(ci => ci.ProductColorId == productColor.Id && ci.SizeId == sizeId);
                if (itemToRemove != null)
                {
                    cart.CartItems.Remove(itemToRemove);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }
            else
            {
                var cart = GetCartFromSession();

                var itemToRemove = cart.CartItems
                    .FirstOrDefault(ci => ci.ProductColor.ProductId == productId && ci.ProductColor.ColorId == colorId && ci.SizeId == sizeId);
                if (itemToRemove != null)
                {
                    cart.CartItems.Remove(itemToRemove);
                    SaveCartToSession(cart);
                }

                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> GetCartTotal()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.ProductColor)
                            .ThenInclude(pc => pc.Product)
                    .SingleOrDefaultAsync(c => c.UserId == userId);

                var total = cart.CartItems.Sum(ci => ci.ProductColor.Product.Price * ci.Quantity);
                return Json(new { total = total });
            }
            else
            {
                var cart = GetCartFromSession();
                var total = cart.CartItems.Sum(ci => ci.ProductColor.Product.Price * ci.Quantity);
                return Json(new { total = total });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductColor)
                        .ThenInclude(pc => pc.Product)
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Size)
                .SingleOrDefaultAsync(c => c.UserId == userId);

            if (cart.CartItems.Count() == 0)
            {
                return RedirectToAction("Index");
            }

            // Tính tổng tiền VND
            var tongTienVnd = cart.CartItems.Sum(p => p.ProductColor.Product.Price * p.Quantity);

            // Lấy tỷ giá USD sang VND
            var vndToUsdRate = await _exchangeRateProvider.GetVndToUsdRateAsync();
            var tongTienUsd = Math.Round(tongTienVnd * vndToUsdRate, 2);

            ViewBag.TongTienUsd = tongTienUsd;
            
            ViewBag.PaypalClientId = _paypalClient.ClientId;
            return View(cart);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckOutViewModel checkOutViewModel, string payment = "Đặt hàng (COD)")
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = await _userManager.FindByIdAsync(userId);

                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.ProductColor)
                            .ThenInclude(pc => pc.Product)
                    .SingleOrDefaultAsync(c => c.UserId == userId);

                if (payment == "Thanh toán VnPay")
                {
                    var vnPayModel = new VnPaymentRequestModel
                    {
                        Amount = (double)cart.CartItems.Sum(ci => ci.ProductColor.Product.Price),
                        CreatedDate = DateTime.Now,
                        Description = $"{user.LastName} {user.FirstName} {user.PhoneNumber} {user.Email}",
                        FullName = $"{user.LastName} {user.FirstName}",
                        OrderId = new Random().Next(1000, 100000)
                    };
                    return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
                }


                var order = new Order
                {
                    FirstName = checkOutViewModel.FirstName ?? user.FirstName,
                    LastName = checkOutViewModel.LastName ?? user.LastName,
                    Address = checkOutViewModel.Address ?? user.Address,
                    PhoneNumber = checkOutViewModel.PhoneNumber ?? user.PhoneNumber,
                    Email = checkOutViewModel.Email ?? user.Email,
                    Note = checkOutViewModel.Note,
                    PaymentMethod = "COD",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    UserId = userId,
                    OrderStatusId = GetOrderStatusIdByName("Đang chờ xác nhận")
                };

                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    await _context.AddAsync(order);
                    await _context.SaveChangesAsync();

                    var orderItems = new List<OrderItem>();
                    foreach (var item in cart.CartItems)
                    {
                        orderItems.Add(new OrderItem
                        {
                            Id = item.Id,
                            Quantity = item.Quantity,
                            ProductColorId = item.ProductColorId,
                            ProductColor = item.ProductColor,
                            SizeId = item.SizeId,
                            Size = item.Size,
                            OrderId = order.Id,
                            Order = order,
                        });
                    }
                    
                    await _context.AddRangeAsync(orderItems);

                    var cartItems = await _context.CartItems
                        .Where(ci => ci.Cart.UserId == userId)
                        .ToListAsync();
                    _context.CartItems.RemoveRange(cartItems);

                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();

                    return View("Success");
                }
                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                    return View("Failed");
                }
            }

            return View("Index");
        }

        private Guid? GetOrderStatusIdByName(string orderName)
        {
            var orderStatus = _context.OrderStatuses.AsNoTracking().FirstOrDefault(os => os.Name == orderName);
            return orderStatus?.Id;
        }

        #region Paypal Payment
        [Authorize]
        [HttpPost("/Cart/create-paypal-order")]
        public async Task<IActionResult> CreatePaypalOrder(CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userManager.FindByIdAsync(userId);

            var cart = await _context.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.ProductColor)
                            .ThenInclude(pc => pc.Product)
                    .SingleOrDefaultAsync(c => c.UserId == userId);

            // Thông tin đơn hàng gửi qua Paypal
            var tongTien = cart.CartItems.Sum(p => p.ProductColor.Product.Price);


            // Lấy tỷ giá USD sang VND
            var vndToUsdRate = await _exchangeRateProvider.GetVndToUsdRateAsync();

            var tongTienUsd = Math.Round(tongTien * vndToUsdRate, 2).ToString();

            var donViTienTe = "USD";
            var maDonHangThamChieu = "DH" + DateTime.Now.Ticks.ToString();

            try
            {
                var response = await _paypalClient.CreateOrder(tongTienUsd, donViTienTe, maDonHangThamChieu);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new { ex.GetBaseException().Message };
                return BadRequest(error);
            }
        }

        [Authorize]
        [HttpPost("/Cart/capture-paypal-order")]
        public async Task<IActionResult> CapturePaypalOrder(CheckOutViewModel checkOutViewModel, string orderId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderId);

                // Lưu vào Database đơn hàng của mình
                if (ModelState.IsValid)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    var user = await _userManager.FindByIdAsync(userId);

                    var cart = await _context.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.ProductColor)
                            .ThenInclude(pc => pc.Product)
                    .SingleOrDefaultAsync(c => c.UserId == userId);

                    var order = new Order
                    {
                        FirstName = checkOutViewModel.FirstName ?? user.FirstName,
                        LastName = checkOutViewModel.LastName ?? user.LastName,
                        Address = checkOutViewModel.Address ?? user.Address,
                        PhoneNumber = checkOutViewModel.PhoneNumber ?? user.PhoneNumber,
                        Email = checkOutViewModel.Email ?? user.Email,
                        Note = checkOutViewModel.Note,
                        PaymentMethod = "PAYPAL",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        IsDeleted = false,
                        UserId = userId,
                        OrderStatusId = GetOrderStatusIdByName("Đang chờ xác nhận")
                    };

                    using var transaction = await _context.Database.BeginTransactionAsync();
                    try
                    {
                        await _context.AddAsync(order);
                        await _context.SaveChangesAsync();

                        var orderItems = new List<OrderItem>();
                        foreach (var item in cart.CartItems)
                        {
                            orderItems.Add(new OrderItem
                            {
                                Id = item.Id,
                                Quantity = item.Quantity,
                                ProductColorId = item.ProductColorId,
                                ProductColor = item.ProductColor,
                                SizeId = item.SizeId,
                                Size = item.Size,
                                OrderId = order.Id,
                                Order = order,
                            });
                        }

                        await _context.AddRangeAsync(orderItems);

                        var cartItems = await _context.CartItems
                            .Where(ci => ci.Cart.UserId == userId)
                            .ToListAsync();
                        _context.CartItems.RemoveRange(cartItems);

                        await _context.SaveChangesAsync();
                        await _context.Database.CommitTransactionAsync();
                    }
                    catch
                    {
                        await _context.Database.RollbackTransactionAsync();
                    }
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new { ex.GetBaseException().Message };
                return BadRequest(error);
            }
        }

        [Authorize]
        public async Task<IActionResult> PaymentSuccess()
        {
            return View("Success");
        }

        [Authorize]
        public async Task<IActionResult> PaymentFailed()
        {
            return View("Failed");
        }

        #endregion

        #region VnPay
        [Authorize]
        public async Task<IActionResult> PaymentCallBack(CheckOutViewModel checkOutViewModel)
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VnPay: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFailed");
            }

            // Lưu vào Database đơn hàng của mình
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = await _userManager.FindByIdAsync(userId);

                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.ProductColor)
                            .ThenInclude(pc => pc.Product)
                    .SingleOrDefaultAsync(c => c.UserId == userId);

                var order = new Order
                {
                    FirstName = checkOutViewModel.FirstName ?? user.FirstName,
                    LastName = checkOutViewModel.LastName ?? user.LastName,
                    Address = checkOutViewModel.Address ?? user.Address,
                    PhoneNumber = checkOutViewModel.PhoneNumber ?? user.PhoneNumber,
                    Email = checkOutViewModel.Email ?? user.Email,
                    Note = checkOutViewModel.Note,
                    PaymentMethod = "VNPAY",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    UserId = userId,
                    OrderStatusId = GetOrderStatusIdByName("Đang chờ xác nhận")
                };

                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    await _context.AddAsync(order);
                    await _context.SaveChangesAsync();

                    var orderItems = new List<OrderItem>();
                    foreach (var item in cart.CartItems)
                    {
                        orderItems.Add(new OrderItem
                        {
                            Id = item.Id,
                            Quantity = item.Quantity,
                            ProductColorId = item.ProductColorId,
                            ProductColor = item.ProductColor,
                            SizeId = item.SizeId,
                            Size = item.Size,
                            OrderId = order.Id,
                            Order = order,
                        });
                    }

                    await _context.AddRangeAsync(orderItems);

                    var cartItems = await _context.CartItems
                        .Where(ci => ci.Cart.UserId == userId)
                        .ToListAsync();
                    _context.CartItems.RemoveRange(cartItems);

                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                }
                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }

            TempData["Message"] = $"Thanh toán VnPay thành công";
            return RedirectToAction("PaymentSuccess");
        }

        #endregion
    }
}
