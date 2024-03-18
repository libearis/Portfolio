using Microsoft.AspNetCore.Authentication.Cookies;
using TrollMarketDataAccess;
using TrollMarketWeb.Configuration;

namespace TrollMarketWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        Dependencies.ConfigureService(builder.Configuration, builder.Services);
        builder.Services.AddBusinessServices();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(opt => {
            opt.Cookie.Name = "AuthCookie";
            opt.LoginPath = "/Login";
            opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            opt.AccessDeniedPath ="/AccessDenied";
        });
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
        
        var app = builder.Build();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=Index}"  
            );

        app.UseStaticFiles();
        app.Run();
    }
}
