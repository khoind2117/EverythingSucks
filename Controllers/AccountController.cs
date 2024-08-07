using EverythingSucks.ViewModels;
using EverythingSucks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EverythingSucks.Data;
using Microsoft.EntityFrameworkCore;

namespace EverythingSucks.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CartController _cartController;

        public AccountController(ApplicationDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.ReturnUrl = returnUrl ?? Url.Content("~/");
            return View(loginViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, isPersistent: true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.GetUserAsync(User); // Get the currently signed-in user

                    // Check if the user has an admin role
                    bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin"); // Replace "Admin" with your actual admin role name

                    if (isAdmin)
                    {
                        // Redirect to the admin area's Index controller
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else
                    {
                        // Redirect to the regular user area
                        return RedirectToAction("Index", "Home");
                    }
                    //await _cartController.MergeCart(); // Di chuyển giỏ hàng từ session sang cơ sở dữ liệu
                    //return RedirectToAction("Index", "Home");
                }
                if (result.IsLockedOut)
                {
                    // Người dùng bị khóa tài khoản
                    return View("Lockout");
                }
                else
                {
                    // Hiển thị lỗi khi người dùng nhập sai mật khẩu
                    ModelState.AddModelError("", "Email hoặc mật khẩu không đúng. Vui lòng thử lại");
                    return View(loginViewModel);
                }
            }
            ModelState.AddModelError("", "Vui lòng kiểm tra lại thông tin đăng nhập");
            return View(loginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> RegisterAsync(string? returnUrl = null)
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.ReturnUrl = returnUrl;
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string? returnUrl = null)
        {
            registerViewModel.ReturnUrl = returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var userByEmail = await _userManager.FindByEmailAsync(registerViewModel.Email);

                // Kiểm tra Username
                if (userByEmail != null)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng, vui lòng sử dụng email khác");
                    return View(registerViewModel);

                }

                // Tạo tài khoản
                if (userByEmail == null)
                {
                    var user = new User
                    {
                        FirstName = registerViewModel.FirstName,
                        LastName = registerViewModel.LastName,
                        UserName = registerViewModel.Email,
                        PhoneNumber = registerViewModel.PhoneNumber,
                        Email = registerViewModel.Email,
                        Address = registerViewModel.Address,
                    };
                    var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                    if (result.Succeeded)
                    {
                        #region Role
                        await _userManager.AddToRoleAsync(user, "User"); // Gán role Admin/User
                        #endregion

                        // Khởi tạo Cart cho người dùng mới
                        var cart = new Cart
                        {
                            Id = Guid.NewGuid(),
                            UserId = user.Id,
                            CartStatusId = GetCartStatusIdByName("Trống")
                        };

                        // Thêm cart vào context và lưu thay đổi
                        _context.Carts.Add(cart);
                        await _context.SaveChangesAsync();

                        // Gán CartId cho User
                        user.CartId = cart.Id;
                        _context.Users.Update(user);

                        // Lưu thay đổi cho User
                        await _context.SaveChangesAsync();

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }

                // Nếu tạo không thành công sẽ trả về
                // Có thể là do mật khẩu chưa đúng quy định, cần làm rõ hơn cho người dùng
                ModelState.AddModelError("Password", "Tạo tài khoản không thành công");
            }
            return View(registerViewModel);
        }

        private Guid? GetCartStatusIdByName(string statusName)
        {
            var cartStatus = _context.CartStatuses.AsNoTracking().FirstOrDefault(cs => cs.Name == statusName);
            return cartStatus?.Id;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
