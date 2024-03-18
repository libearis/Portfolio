using TrollMarketBusiness.Repositories;
using TrollMarketBusiness.Interfaces;
using TrollMarketWeb.Services;

namespace TrollMarketWeb.Configuration;

public static class ConfigureBusinessService
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IBuyerRepository, BuyerRepository>();
        services.AddScoped<ISellerRepository, SellerRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IShopRepository, ShopRepository>();
        services.AddScoped<IShipmentRepository, ShipmentRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<AccountService>();
        services.AddScoped<BuyerService>();
        services.AddScoped<SellerService>();
        services.AddScoped<ShopService>();
        services.AddScoped<CartService>();
        services.AddScoped<MerchandiseService>();
        services.AddScoped<AdminService>();
        services.AddScoped<HistoryService>();

        return services;
    }
}
