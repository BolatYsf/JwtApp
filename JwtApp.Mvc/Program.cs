using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";
    //strict cookie sadece ilgili domain uzerinde cal�smas�n� saglar 
    opt.Cookie.SameSite = SameSiteMode.Strict;
    //ilgili cookie js ile paylas�lmas�n� engeller
    opt.Cookie.HttpOnly = true;
    //sadece https ile gonderileri alacak cookie
    opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    opt.Cookie.Name = "JwtApp";
});


var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
