using EverythingSucks.Data;
using EverythingSucks.Helpers;
using EverythingSucks.Models;
using EverythingSucks.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<PhotoService>();

#region Cloudinary
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
#endregion

#region ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Cấu hình quy định mật khẩu
    options.Password.RequiredLength = 6; // Chiều dài tối thiểu là 6 ký tự
    options.Password.RequireNonAlphanumeric = false; // Không yêu cầu ký tự không phải chữ cái
    options.Password.RequireUppercase = false; // Yêu cầu chữ hoa
    options.Password.RequireLowercase = false; // Yêu cầu chữ thường
    options.Password.RequireDigit = false; // Yêu cầu số
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
    //options.Lockout.MaxFailedAccessAttempts = 5;
    //options.SignIn.RequireConfirmedAccount = true; yêu cầu người dùng xác nhận tài khoản
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
#endregion

#region Cookie & Session
builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian timeout cho session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseDeveloperExceptionPage();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");   
app.Run();
