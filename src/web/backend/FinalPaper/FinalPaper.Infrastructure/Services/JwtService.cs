using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FinalPaper.Domain.Entities;
using FinalPaper.Domain.Enums;
using FinalPaper.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace FinalPaper.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration configuration;
    private readonly ILogger<JwtService> logger;
    private readonly SymmetricSecurityKey secretKey;
    private readonly SigningCredentials signinCredentials;

    public JwtService(IConfiguration configuration, ILogger<JwtService> logger)
    {
        this.configuration = configuration;
        this.logger = logger;
        secretKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException()));
        signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
    }

    public string GenerateJwtToken(in User user)
    {
        // TODO: [el] Currently all users will have Admin access. This should be updated!
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Role, Roles.Admin.Name)
        };

        var jwtToken = new JwtSecurityToken(claims: claims,
            issuer: string.IsNullOrEmpty(configuration["Jwt:Issuer"])
                ? null
                : configuration["Jwt:Issuer"],
            audience: string.IsNullOrEmpty(configuration["Jwt:Audience"])
                ? null
                : configuration["Jwt:Audience"],
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signinCredentials);

        var json = JsonConvert.SerializeObject(user, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        jwtToken.Payload.Add("user", json);
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    public JwtSecurityToken? ValidateJwtToken(in string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = secretKey,
                ValidateIssuer = string.IsNullOrEmpty(configuration["Jwt:Issuer"]) ? false : true,
                ValidateAudience = string.IsNullOrEmpty(configuration["Jwt:Audience"]) ? false : true,
                ValidIssuer = string.IsNullOrEmpty(configuration["Jwt:Issuer"])
                    ? null
                    : configuration["Jwt:Issuer"],
                ValidAudience = string.IsNullOrEmpty(configuration["Jwt:Audience"])
                    ? null
                    : configuration["Jwt:Audience"],
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
        catch
        {
            logger.LogError($"Invalid JWT token {token}!");
        }

        return null;
    }

    public RefreshToken CreateRefreshToken(int expirationDays = 10)
    {
        var randomNumber = new byte[32];
        var generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomNumber);
        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomNumber),
            ExpiresDateUtc = DateTime.UtcNow.AddDays(expirationDays),
            CreatedDateUtc = DateTime.UtcNow
        };
    }

    public T? ReadJwtToken<T>(in string token, in bool validate = false) where T : class
    {
        if (string.IsNullOrEmpty(token))
            return null;

        JwtSecurityToken? jwtSecurityToken;
        object? payloadData;

        if (validate)
        {
            jwtSecurityToken = ValidateJwtToken(token);
            if (jwtSecurityToken is null)
                return null;

            jwtSecurityToken.Payload.TryGetValue("user", out payloadData);
        }
        else
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken? securityToken = null;
            try
            {
                securityToken = tokenHandler.ReadToken(token);
            }
            catch (Exception ex)
            {
                logger.LogError($"Invalid JWT token {token}! {ex.Message}");
            }

            if (securityToken is null)
                return null;
            var jwtToken = securityToken as JwtSecurityToken;
            if (jwtToken is null)
                return null;

            jwtToken.Payload.TryGetValue("user", out payloadData);
        }

        if (payloadData is null)
            return null;

        if (typeof(T) == typeof(string))
            return (T)payloadData;

        try
        {
            return JsonConvert.DeserializeObject<T>($"{payloadData}");
        }
        catch (Exception ex)
        {
            logger.LogError($"Invalid JWT token {token}! {ex.Message}");
        }

        return null;
    }
}