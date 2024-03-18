using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrollMarketDataAccess.Models;

namespace TrollMarketDataAccess;

public static class Dependencies
{
    public static void ConfigureService(IConfiguration configuration, IServiceCollection service)
    {
        service.AddDbContext<TrollMarketContext>(
            option => option.UseSqlServer(configuration.GetConnectionString("TrollMarketConnection"))
        );
    }
}
