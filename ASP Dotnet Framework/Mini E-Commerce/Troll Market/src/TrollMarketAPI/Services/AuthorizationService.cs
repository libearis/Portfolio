using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TrollMarketAPI.Authorization;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketAPI.Services;

public class AuthorizationService
{
    private readonly IAccountRepository repository;
    private readonly IConfiguration configuration;

    public AuthorizationService(IAccountRepository repository, IConfiguration configuration)
    {
        this.repository = repository;
        this.configuration = configuration;
    }

    private AuthResponseDTO CreateToken(Account model)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, model.Username),
            new Claim(ClaimTypes.Role, model.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value)
            );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims : claims,
            expires : DateTime.Now.AddDays(1),
            signingCredentials : credentials
            );

        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return new AuthResponseDTO()
        {
            Token = serializedToken
        };
    }
    public AuthResponseDTO GetToken(AuthRequestDTO requestDTO)
    {
        var model = repository.GetAccount(requestDTO.Username);
        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(requestDTO.Password, model.Password);
        if(isCorrectPassword)
        {
            return CreateToken(model);
        }
        throw new Exception("Username tidak ada");
    }
}
