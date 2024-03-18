using WinterholdAPI.Services;
using WinterholdBusiness.Interfaces;
using WinterholdBusiness.Repositories;

namespace WinterholdAPI.Configuration;

public static class ConfigureBusinessServices
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<CategoryService>();
        services.AddScoped<BookService>();
        services.AddScoped<CustomerService>();
        services.AddScoped<LoanService>();
        return services;
    }
}
