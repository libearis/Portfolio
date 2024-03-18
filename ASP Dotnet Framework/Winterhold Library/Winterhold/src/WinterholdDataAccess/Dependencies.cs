using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WinterholdDataAccess.Models;

namespace WinterholdDataAccess;

public static class Dependencies
{
    public static void ConfigureService(IConfiguration configuration, IServiceCollection service)
    {
        service.AddDbContext<WinterholdContext>(
            option => option.UseSqlServer(configuration.GetConnectionString("WinterholdConnection"))
        );
    }
}
