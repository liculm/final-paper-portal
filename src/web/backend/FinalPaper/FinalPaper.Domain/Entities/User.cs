using FinalPaper.Domain.Entities.Base;
using FinalPaper.Domain.Enums;

namespace FinalPaper.Domain.Entities; 

public class User : Entity
{
    public User()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }
    
    public int Id { get; set; }
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
    
    public void Update(string firstName, string lastName, bool isActive, int roleId) 
    {
        FirstName = firstName;
        LastName = lastName;
        IsActive = isActive;
        RoleId = roleId;
    }
}
