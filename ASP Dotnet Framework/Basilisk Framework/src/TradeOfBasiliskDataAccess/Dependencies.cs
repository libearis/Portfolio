using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TradeOfBasiliskDataAccess.Models;

namespace TradeOfBasiliskDataAccess;

public static class Dependencies
{
    public static void ConfigureService(IConfiguration configuration, IServiceCollection service)
    {
        service.AddDbContext<BasiliskTFContext>(
            option => option.UseSqlServer(configuration.GetConnectionString("BootcampConnection"))
        );
    }
}
