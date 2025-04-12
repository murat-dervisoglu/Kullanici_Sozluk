using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

// **Session ekliyoruz**
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // GDPR uyumluluðu için gerekli olabilir
});

// **Kimlik doðrulama ekliyoruz**
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/WriterLogin"; // Giriþ yapýlmamýþsa yönlendirme yapýlacak yer
        //options.AccessDeniedPath = "/Login/AccessDenied"; // Yetki hatasýnda yönlendirme yapýlacak yer
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Çerez süresi
});

// **Yetkilendirme politikasý ekliyoruz**
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireLoggedIn", policy =>
    {
        policy.RequireAuthenticatedUser(); // Bu politika giriþ yapýlmýþ kullanýcýlarý zorunlu kýlar
    });
});

// **MVC filtreleri için Authorize ekliyoruz**
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Statik dosyalar için middleware

app.UseRouting();

// **Session middleware'i ekliyoruz**
app.UseSession();

// **Kimlik doðrulama ve yetkilendirme middleware'lerini ekliyoruz**
app.UseAuthentication();
app.UseAuthorization();

// **Rota tanýmlamasý**
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
