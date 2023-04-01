using System.Security.Claims;

namespace FinalPaper.Domain.Interfaces; 

public interface IJwtService {
    string GenerateAccessToken(string userId, string refreshToken);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromAccessToken(string token);
}