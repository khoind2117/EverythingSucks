using EverythingSucks.ViewModels;
using EverythingSucks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EverythingSucks.Data;

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
                    //await _cartController.MergeCart(); // Di chuyển giỏ hàng từ session sang cơ sở dữ liệu
                    return RedirectToAction("Index", "Home");
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
                    var user = new User(_context)
                    {
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
