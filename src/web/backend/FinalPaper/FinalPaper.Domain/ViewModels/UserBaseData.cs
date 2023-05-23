namespace FinalPaper.Domain.ViewModels; 

public sealed record UserBaseData(string Id, string FirstName, string LastName, string Username, int RoleId, string RoleName) {
    public string Id { get; set; } = Id;
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;
    public string Username { get; set; } = Username;
    public int RoleId { get; set; } = RoleId;
    public string RoleName { get; set; } = RoleName;
}