using FinalPaper.Domain.Entities;

namespace FinalPaper.Domain.Interfaces; 

public interface IRefreshTokenService {
    Task<RefreshToken> GenerateRefreshTokenAsync(int userId);
    Task<RefreshToken> GetRefreshTokenAsync(string token);
    Task<bool> RevokeRefreshTokenAsync(string token);
}