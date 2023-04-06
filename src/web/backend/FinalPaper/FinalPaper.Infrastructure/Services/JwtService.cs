using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinalPaper.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace FinalPaper.Infrastructure.Services; 

public class JwtService : IJwtService {
    private readonly IConfiguration configuration;
    private readonly  ILogger<JwtService> logger;

    public JwtService(IConfiguration configuration, ILogger<JwtService> logger) {
        this.configuration = configuration;
        this.logger = logger;
    }

    public string GenerateRefreshToken()
    {
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:RefreshTokenSecretKey"] 
                                                                           ?? throw new InvalidOperationException()));
        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Jwt:RefreshTokenExpirationInMinutes"])),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string GenerateAccessToken(string userId, string refreshToken)
    {
        var principal = GetPrincipalFromAccessToken(refreshToken);
        var jti = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"] 
                                                                           ?? throw new InvalidOperationException()));

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Jwt:AccessTokenExpirationInMinutes"])),
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim("kid", Guid.NewGuid().ToString()),
                new Claim("TEst", "jti"),
            }),
            SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    
    public ClaimsPrincipal GetPrincipalFromAccessToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"] 
                                                                           ?? throw new InvalidOperationException()));
        
        try {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = symmetricKey,
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["Jwt:Audience"],
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero// Because the token may not have expiration date in the refresh token scenario
            }, out var validatedToken);

            if (validatedToken is not JwtSecurityToken jwtToken) return null;
            
            var claims = jwtToken.Claims;
            return new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        }
        catch (Exception exception)
        {
            logger.LogError(exception.ToString());
            
            return null;
        }
    }
}