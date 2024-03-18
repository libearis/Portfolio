using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;
using TrollMarketWeb.ViewModels.Account;

namespace TrollMarketWeb.Services;

public class AdminService
{
    private readonly IAccountRepository repository;

    public AdminService(IAccountRepository repository)
    {
        this.repository = repository;
    }

    public void Insert(AdminRegisterVM viewModel)
    {
        var model = new Account()
        {
            Username = viewModel.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(viewModel.Password),
            Role = "Admin"
        };

        repository.Insert(model);
    }
}
