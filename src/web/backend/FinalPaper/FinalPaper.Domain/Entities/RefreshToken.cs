namespace FinalPaper.Domain.Entities;

public sealed class RefreshToken
{
    public Guid UserId { get; set; }
    public string? Token { get; set; }
    public DateTime ExpiresDateUtc { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresDateUtc;
    public DateTime CreatedDateUtc { get; set; }
    public DateTime? RevokedDateUtc { get; set; }
    public bool IsActive => RevokedDateUtc == null && !IsExpired;
    public User? User { get; set; }

    public void Update(RefreshToken refreshToken)
    {
        Token = refreshToken.Token;
        ExpiresDateUtc = refreshToken.ExpiresDateUtc;
        CreatedDateUtc = refreshToken.CreatedDateUtc;
        RevokedDateUtc = null;
    }

    public void Revoke()
    {
        RevokedDateUtc = DateTime.UtcNow;
    }
}