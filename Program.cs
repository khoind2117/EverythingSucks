using EverythingSucks.Controllers;
using EverythingSucks.Data;
using EverythingSucks.Helpers;
using EverythingSucks.Models;
using EverythingSucks.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Twilio.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
});
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true; // Cấu hình tạo ra URL dạng chữ thường
});

#region Cloudinary
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<PhotoService>();
#endregion

#region Paypal
builder.Services.AddSingleton(x => new PaypalClient(
    builder.Configuration["PaypalOptions:AppId"],
    builder.Configuration["PaypalOptions:AppSecret"],
    builder.Configuration["PaypalOptions:Mode"]
));
#endregion

#region FloatRatesExchange
builder.Services.AddScoped<IExchangeRateProvider, FloatRatesExchangeRateProvider>();
#endregion

#region VnPay
builder.Services.AddSingleton<IVnPayService, VnPayService>();
#endregion

#region Twillio
builder.Services.AddHttpClient<ITwilioRestClient, TwilioClient>();
var smsApiUrl = builder.Configuration["SmsApiUrl"];
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
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
#endregion

#region Cookie & Session
builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // Thời gian timeout cho session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie();
#endregion

builder.Services.AddScoped<CartController>();
builder.Services.AddHttpContextAccessor();

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

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.UseDeveloperExceptionPage();

app.MapControllerRoute(
    name: "Area_route",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
