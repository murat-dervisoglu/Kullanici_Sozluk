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
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum s�resi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // GDPR uyumlulu�u i�in gerekli olabilir
});

// **Kimlik do�rulama ekliyoruz**
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/WriterLogin"; // Giri� yap�lmam��sa y�nlendirme yap�lacak yer
        //options.AccessDeniedPath = "/Login/AccessDenied"; // Yetki hatas�nda y�nlendirme yap�lacak yer
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // �erez s�resi
});

// **Yetkilendirme politikas� ekliyoruz**
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireLoggedIn", policy =>
    {
        policy.RequireAuthenticatedUser(); // Bu politika giri� yap�lm�� kullan�c�lar� zorunlu k�lar
    });
});

// **MVC filtreleri i�in Authorize ekliyoruz**
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
app.UseStaticFiles(); // Statik dosyalar i�in middleware

app.UseRouting();

// **Session middleware'i ekliyoruz**
app.UseSession();

// **Kimlik do�rulama ve yetkilendirme middleware'lerini ekliyoruz**
app.UseAuthentication();
app.UseAuthorization();

// **Rota tan�mlamas�**
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
