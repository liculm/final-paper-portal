using System.IdentityModel.Tokens.Jwt;
using FinalPaper.Domain.Entities;

namespace FinalPaper.Domain.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(in string name, in dynamic payload);
    JwtSecurityToken? ValidateJwtToken(in string token);
    RefreshToken CreateRefreshToken(int expirationDays = 10);
    T? ReadJwtToken<T>(in string token, in string payloadName, in bool validate = false) where T : class;
}