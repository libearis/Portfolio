using TrollMarketAPI.Services;
using TrollMarketBusiness.Interfaces;
using TrollMarketBusiness.Repositories;
using TrollMarketDataAccess.Models;

namespace TrollMarketAPI.Configuration;

public static class ConfigureBusinessService
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IShopRepository, ShopRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IBuyerRepository, BuyerRepository>();
        services.AddScoped<ISellerRepository, SellerRepository>();
        services.AddScoped<IShipmentRepository, ShipmentRepository>();
        services.AddScoped<ShopService>();
        services.AddScoped<AuthorizationService>();
        services.AddScoped<MerchandiseService>();
        services.AddScoped<ShipmentService>();

        return services;
    }
}
