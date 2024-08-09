using EverythingSucks.ViewModels;
using EverythingSucks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EverythingSucks.Data;
using Microsoft.EntityFrameworkCore;
using EverythingSucks.Services;
using Newtonsoft.Json;
using System.Text;

namespace EverythingSucks.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CartController _cartController;
        private readonly string _smsApiUrl;

        public AccountController(ApplicationDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            CartController cartController,
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _cartController = cartController;
            _smsApiUrl = configuration["SmsApiUrl"];
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
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        if (await _userManager.IsInRoleAsync(user, "Admin"))
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            // Chuyển hướng đến trang quản trị
                            return RedirectToAction("Index", "Home", new { Area = "Admin" });
                        }

                        // Tạo mã xác thực cho đăng nhập
                        var verificationCode = new Random().Next(100000, 999999).ToString();
                        HttpContext.Session.SetString("VerificationCode", verificationCode);
                        HttpContext.Session.SetString("UserId", user.Id);
                        HttpContext.Session.SetString("ReturnUrl", returnUrl);
                        HttpContext.Session.SetString("ActionType", "Login");

                        // Gửi mã xác thực qua SMS
                        var smsMessage = new SmsMessage
                        {
                            To = ConvertToInternationalFormat(user.PhoneNumber),
                            From = "+17627012826", // Số điện thoại Twilio của bạn
                            Message = $"Your verification code is {verificationCode}"
                        };

                        var httpClient = new HttpClient();
                        var content = new StringContent(JsonConvert.SerializeObject(smsMessage), Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(_smsApiUrl, content);
                        response.EnsureSuccessStatusCode();

                        // Chuyển hướng đến trang xác thực mã
                        return RedirectToAction("VerifyCode", new { ReturnUrl = returnUrl });
                    }
                    else
                    {
                        // Hiển thị lỗi khi người dùng nhập sai mật khẩu
                        ModelState.AddModelError("", "Email hoặc mật khẩu không đúng. Vui lòng thử lại");
                        return View(loginViewModel);
                    }
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

                // Tạo mã xác thực
                var verificationCode = new Random().Next(100000, 999999).ToString();
                HttpContext.Session.SetString("VerificationCode", verificationCode);
                HttpContext.Session.SetString("PhoneNumber", registerViewModel.PhoneNumber);
                HttpContext.Session.SetString("Email", registerViewModel.Email);
                HttpContext.Session.SetString("FirstName", registerViewModel.FirstName);
                HttpContext.Session.SetString("LastName", registerViewModel.LastName);
                HttpContext.Session.SetString("Address", registerViewModel.Address);
                HttpContext.Session.SetString("Password", registerViewModel.Password);
                HttpContext.Session.SetString("ActionType", "Register");

                // Gửi mã xác thực qua SmsController
                var smsMessage = new SmsMessage
                {
                    To = ConvertToInternationalFormat(registerViewModel.PhoneNumber), // Chuyển đổi số điện thoại sang định dạng quốc tế
                    From = "+17627012826", // Số điện thoại Twilio của bạn
                    Message = $"Your verification code is {verificationCode}"
                };

                // Gửi yêu cầu POST đến SmsController
                var httpClient = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(smsMessage), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(_smsApiUrl, content);

                // Khả năng là thiếu Định dạng quốc tế (+)
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API call failed with status code {response.StatusCode}: {errorMessage}");
                }

                response.EnsureSuccessStatusCode();
                // Chuyển hướng đến trang xác thực mã
                return RedirectToAction("VerifyCode");
            }
            return View(registerViewModel);
        }

        public string ConvertToInternationalFormat(string localNumber)
        {
            if (localNumber.StartsWith("0"))
            {
                return $"+84{localNumber.Substring(1)}";
            }
            return localNumber;
        }

        [HttpGet]
        public IActionResult VerifyCode(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            // Lấy mã xác thực và loại hành động từ Session
            var savedCode = HttpContext.Session.GetString("VerificationCode");
            var actionType = HttpContext.Session.GetString("ActionType");
            var returnUrl = HttpContext.Session.GetString("ReturnUrl");

            if (model.Code == savedCode && actionType == "Register")
            {
                // Lấy thông tin người dùng từ Session
                var phoneNumber = HttpContext.Session.GetString("PhoneNumber");
                var email = HttpContext.Session.GetString("Email");
                var firstName = HttpContext.Session.GetString("FirstName");
                var lastName = HttpContext.Session.GetString("LastName");
                var address = HttpContext.Session.GetString("Address");
                var password = HttpContext.Session.GetString("Password");

                // Mã xác thực đúng, tạo tài khoản cho người dùng
                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = email,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Address = address
                };

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");

                    var cart = new Cart
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        CartStatusId = GetCartStatusIdByName("Trống")
                    };

                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();

                    user.CartId = cart.Id;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();

                    // Save cart to database
                    await _cartController.SaveCartToDatabaseAsync(user.Id);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    HttpContext.Session.Clear(); // Xóa dữ liệu Session sau khi sử dụng
                    return View("2FA_Success"); // Hoặc trang nào đó sau khi đăng ký thành công
                }

                return View("2FA_Failed");
            }
            else if (model.Code == savedCode && actionType == "Login")
            {
                var userId = HttpContext.Session.GetString("UserId");

                if (userId != null)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        // Save cart to database
                        await _cartController.SaveCartToDatabaseAsync(user.Id);
                        await _signInManager.SignInAsync(user, false);
                        HttpContext.Session.Clear(); // Xóa dữ liệu Session sau khi sử dụng
                        return Redirect(returnUrl ?? Url.Content("~/"));
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Code", "Mã xác thực không chính xác.");
            }

            return View(model);
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
            return RedirectToAction("Login", "Account");
        }
    }
}
