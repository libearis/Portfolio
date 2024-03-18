using WinterholdDataAccess;
using WinterholdWeb.Configuration;

namespace WinterholdWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        Dependencies.ConfigureService(builder.Configuration, builder.Services);
        builder.Services.AddBusinessServices();
        
        var app = builder.Build();
        app.UseRouting();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}"  
            );

        app.UseStaticFiles();
        app.Run();
    }
}
