using BasiliskBusiness.Interfaces;
using BasiliskBusiness.Repositories;
using BasiliskWeb.Services;

namespace BasiliskWeb.Configuration;

public static class ConfigurationBusinessServices
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IDeliveryRepository, DeliveryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<CategoryService>();
        services.AddScoped<SupplierService>();
        services.AddScoped<CustomerService>();
        services.AddScoped<DeliveryService>();
        services.AddScoped<ProductService>();
        services.AddScoped<OrderService>();
        services.AddScoped<AccountService>();
        return services;
    }
}
