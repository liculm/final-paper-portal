using FinalPaper.Domain.Entities.Base;

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
    public bool IsActive { get; set; }
    public int RoleId { get; set; }
    
    public void Update(string firstName, string lastName, bool isActive, int roleId) 
    {
        FirstName = firstName;
        LastName = lastName;
        IsActive = isActive;
        RoleId = roleId;
    }
}
