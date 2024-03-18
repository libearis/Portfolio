using WinterholdBusiness.Interfaces;
using WinterholdBusiness.Repositories;
using WinterholdWeb.Services;

namespace WinterholdWeb.Configuration;

public static class ConfigureBusinessServices
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<AuthorService>();
        services.AddScoped<CategoryService>();
        services.AddScoped<BookService>();
        services.AddScoped<CustomerService>();
        return services;
    }
}
