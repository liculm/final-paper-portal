using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinalPaper.Domain.Entities;
using FinalPaper.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FinalPaper.Infrastructure.Services; 

public class RefreshTokenService : IRefreshTokenService
{
    private readonly FinalPaperDBContext context;
    private readonly IConfiguration configuration;
    
    public RefreshTokenService(FinalPaperDBContext context, IConfiguration configuration)
    {
        this.context = context;
        this.configuration = configuration;
    }
    
    public async Task<RefreshToken> GenerateRefreshTokenAsync(int userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"] ?? string.Empty);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration["Jwt:RefreshTokenLifetime"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration["Jwt:RefreshTokenLifetime"]))
        };
        context.RefreshToken.Add(refreshToken);
        await context.SaveChangesAsync();
        return refreshToken;
    }
    
    public async Task<RefreshToken> GetRefreshTokenAsync(string token)
    {
        return await context.RefreshToken.FirstOrDefaultAsync(rt => rt.Token == token);
    }
    
    public async Task<bool> RevokeRefreshTokenAsync(string token)
    {
        var refreshToken = await context.RefreshToken.FirstOrDefaultAsync(rt => rt.Token == token);
        if (refreshToken == null) return false;
        context.RefreshToken.Remove(refreshToken);
        await context.SaveChangesAsync();
        return true;
    }
}
