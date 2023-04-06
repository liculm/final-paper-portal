namespace FinalPaper.Domain.Entities;

public sealed class RefreshToken
{
    public RefreshToken()
    {
        Token = string.Empty;
    }

    public string Token { get; set; }
    public DateTime ExpiresDateUtc { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresDateUtc;
    public DateTime CreatedDateUtc { get; set; }
    public DateTime? RevokedDateUtc { get; set; }
    public bool IsActive => RevokedDateUtc == null && !IsExpired;
    public string? ReplacedByToken { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}