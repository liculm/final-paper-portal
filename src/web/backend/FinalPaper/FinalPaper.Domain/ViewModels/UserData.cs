using FinalPaper.Domain.Entities;

namespace FinalPaper.Domain.ViewModels;

public sealed record UserData(string JwtToken,
    Guid Id,
    string FirstName,
    string LastName,
    string Username,
    int RoleId,
    RefreshToken? RefreshToken,
    string RoleName
    ) {
    public string JwtToken { get; set; } = JwtToken;
    public Guid Id { get; set; } = Id;
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;
    public string Username { get; set; } = Username;
    public int RoleId { get; set; } = RoleId;
    public RefreshToken? RefreshToken { get; set; } = RefreshToken;
    public string RoleName { get; set; } = RoleName;
}