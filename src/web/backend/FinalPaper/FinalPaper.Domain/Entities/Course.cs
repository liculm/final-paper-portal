using FinalPaper.Domain.Entities.Base;
using FinalPaper.Domain.Enums;

namespace FinalPaper.Domain.Entities;

public class Course : Entity
{
    public Course()
    {
        Name = string.Empty;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public Guid? MentorId { get; set; }
    public int CourseTypeId { get; set; }
    public User? User { get; set; }
    public CourseTypes? CourseType { get; set; }
    public ICollection<Thesis>? Theses { get; set; }

    public void Update(string name, bool isActive, Guid mentorId, int courseTypeId)
    {
        Name = name;
        IsActive = isActive;
        MentorId = mentorId;
        CourseTypeId = courseTypeId;
    }
}