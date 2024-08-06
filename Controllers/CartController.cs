using EverythingSucks.Data;
using EverythingSucks.Models;
using EverythingSucks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using EverythingSucks.Helpers;

namespace EverythingSucks.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
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
                var userId = User.Identity.Name;
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductColor)
                    .ThenInclude(pc => pc.Product)
                    .SingleOrDefaultAsync(c => c.UserId == userId) ?? new Cart { UserId = userId };

                if (cart.Id == Guid.Empty)
                {
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }

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
                var userId = User.Identity.Name;
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductColor)
                    .ThenInclude(pc => pc.Product)
                    .ThenInclude(ci => ci.ProductColors)
                    .ThenInclude(pc => pc.ProductImages)
                    .SingleOrDefaultAsync(c => c.UserId == userId) ?? new Cart { UserId = userId };

                if (cart.Id == Guid.Empty)
                {
                    _context.Carts.Add(cart);
                }

                var existingItem = cart.CartItems
                        .SingleOrDefault(ci => ci.ProductColor?.ProductId == productId && ci.ProductColor?.ColorId == colorId && ci.SizeId == sizeId);

                if (existingItem == null)
                {
                    var productColor = await _context.ProductColors
                        .SingleOrDefaultAsync(pc => pc.ProductId == productId && pc.ColorId == colorId);

                    if (productColor == null)
                    {
                        return NotFound(); // Hoặc trang lỗi
                    }

                    var newItem = new CartItem
                    {
                        ProductColorId = productColor.Id,
                        Quantity = quantity,
                        SizeId = sizeId,
                        Cart = cart
                    };

                    cart.CartItems.Add(newItem);
                }
                else
                {
                    existingItem.Quantity += quantity;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                var cart = GetCartFromSession();
                cart.CartItems ??= new List<CartItem>();

                var productColor = await _context.ProductColors
                    .Include(pc => pc.ProductImages)
                    .Include(pc => pc.Product)
                    .SingleOrDefaultAsync(pc => pc.ProductId == productId && pc.ColorId == colorId);
                
                var size = await _context.Sizes
                    .SingleOrDefaultAsync(s => s.Id == sizeId);

                if (productColor != null)
                {
                    var existingItem = cart.CartItems
                        .SingleOrDefault(ci => ci.ProductColor?.ProductId == productId && ci.ProductColor?.ColorId == colorId && ci.SizeId == sizeId);

                    if (existingItem != null)
                    {
                        existingItem.Quantity += quantity;
                    }
                    else
                    {
                        cart.CartItems.Add(new CartItem
                        {
                            ProductColorId = productColor.Id,
                            ProductColor = productColor,
                            Quantity = quantity,
                            SizeId = size.Id,
                            Size = size
                        });
                    }

                    SaveCartToSession(cart);
                }

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart([FromBody] List<CartItemUpdateViewModel> cartItems)
        {
            var cart = GetCartFromSession();

            foreach (var item in cartItems)
            {
                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductColor.ProductId == item.ProductId && ci.ProductColor.ColorId == item.ColorId);
                if (cartItem != null)
                {
                    cartItem.Quantity = item.Quantity;
                }
            }

            SaveCartToSession(cart);

            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult RemoveFromCart(Guid productId, Guid colorId)
        {
            var cart = GetCartFromSession();
            var itemToRemove = cart.CartItems.FirstOrDefault(ci => ci.ProductColor.ProductId == productId && ci.ProductColor.ColorId == colorId);

            if (itemToRemove != null)
            {
                cart.CartItems.Remove(itemToRemove);
                SaveCartToSession(cart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult GetCartTotal()
        {
            var cart = GetCartFromSession();
            var total = cart.CartItems.Sum(item => item.ProductColor.Product.Price * item.Quantity);
            return Json(new { total = total });
        }
    }
}
