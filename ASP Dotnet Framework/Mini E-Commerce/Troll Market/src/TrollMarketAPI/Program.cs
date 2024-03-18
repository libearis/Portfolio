using TrollMarketDataAccess;
using TrollMarketAPI.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace TrollMarketAPI;

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
        // Dependencies.ConfigureService(configuration, services);
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddBusinessServices();
        services.AddControllersWithViews();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt => {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)
                        ),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }; 
            });

        services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme{
                    Description = "Example value = Bearer token",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                opt.OperationFilter<SecurityRequirementsOperationFilter>();
            });

        var app = builder.Build();

        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors(MyAllowSpesificOrigin);

        app.Run();
    }
}
