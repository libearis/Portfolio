using BasiliskWeb.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using TradeOfBasiliskDataAccess;
namespace BasiliskWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IServiceCollection services = builder.Services;
        builder.Services.AddControllersWithViews();
        Dependencies.ConfigureService(builder.Configuration, builder.Services);
        services.AddBusinessServices();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(opt => {
            opt.Cookie.Name = "AuthCookie";
            opt.LoginPath = "/Account/Login";
            opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            opt.AccessDeniedPath ="/Account/AccessDenied";
        });
        services.AddAuthorization();
        
        var app = builder.Build();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}"  
            );
        
        app.UseStaticFiles();
        app.Run();
    }
}
