using FinalPaper.Domain.Entities.Base;
using FinalPaper.Domain.Enums;

namespace FinalPaper.Domain.Entities;

public class User : Entity
{
    public User()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Password = string.Empty;
        Username = string.Empty;
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public bool IsActive { get; set; }
    public int RoleId { get; set; }
    public Roles? Role { get; set; }
    public ICollection<Thesis>? Theses { get; set; }
    public ICollection<Course>? Courses { get; set; }
    public ICollection<ThesisDefenceUser>? ThesesDefenceUsers { get; set; }
    public RefreshToken? RefreshToken { get; set; }

    public void UpdateUserInfo(string firstName, string lastName, string username, int roleId)
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        RoleId = roleId;
    }

    public void UpdateUser(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Password = user.Password;
        Username = user.Username;
        IsActive = user.IsActive;
        RoleId = user.RoleId;
    }
    
    public void MarkUserInactive() => IsActive = false;
}