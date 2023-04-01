using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinalPaper.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FinalPaper.Infrastructure.Services; 

public class JwtService : IJwtService {
    private readonly IConfiguration Configuration;

    public JwtService(IConfiguration configuration) {
        Configuration = configuration;
    }

    public string GenerateRefreshToken()
    {
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:RefreshTokenSecretKey"]));
        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        var token = new JwtSecurityToken(
            issuer: Configuration["Jwt:Issuer"],
            audience: Configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(Configuration["Jwt:RefreshTokenExpirationInMinutes"])),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string GenerateAccessToken(string userId, string refreshToken)
    {
        var principal = GetPrincipalFromAccessToken(refreshToken);
        var jti = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]));

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = Configuration["Jwt:Issuer"],
            Audience = Configuration["Jwt:Audience"],
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(Configuration["Jwt:AccessTokenExpirationInMinutes"])),
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, jti)
            }),
            SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    
    public ClaimsPrincipal GetPrincipalFromAccessToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]));

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricKey,
                ValidateIssuer = true,
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = Configuration["Jwt:Audience"],
                ValidateLifetime = false // Because the token may not have expiration date in the refresh token scenario
            }, out SecurityToken validatedToken);

            if (validatedToken != null && validatedToken is JwtSecurityToken jwtToken)
            {
                var claims = jwtToken.Claims;
                return new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            }
            else
            {
                return null;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }
}