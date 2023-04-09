using System.IdentityModel.Tokens.Jwt;
using FinalPaper.Domain.Entities;

namespace FinalPaper.Domain.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(in User user);
    JwtSecurityToken? ValidateJwtToken(in string token);
    RefreshToken CreateRefreshToken(int expirationDays = 10);
    T? ReadJwtToken<T>(in string token, in bool validate = false) where T : class;
}