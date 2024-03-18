using WinterholdDataAccess;
using WinterholdAPI.Configuration;

namespace WinterholdAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var MyAllowSpesificOrigin = "_myAllowSpesificOrigin";
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(opt=>
        {
            opt.AddPolicy(name: MyAllowSpesificOrigin,
            pol=>{
                pol.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod();
            });
        });

        IConfiguration configuration = builder.Configuration;
        IServiceCollection services = builder.Services;
        Dependencies.ConfigureService(configuration, services);
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddBusinessServices();
        services.AddControllersWithViews();

        var app = builder.Build();

        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.MapControllers();
        app.UseCors(MyAllowSpesificOrigin);

        app.Run();
    }
}
