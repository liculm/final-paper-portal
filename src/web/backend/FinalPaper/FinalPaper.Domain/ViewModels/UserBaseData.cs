namespace FinalPaper.Domain.ViewModels; 

public sealed record UserBaseData  {
    public UserBaseData(Guid Id, string firstName, string lastName, string username, int roleId, string roleName) {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        RoleId = roleId;
        RoleName = roleName;
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
}