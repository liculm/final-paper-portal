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
    public int MentorId { get; set; }
    public int CourseTypeId { get; set; }
    public int CourseTypeId2 { get; set; }
    public int CourseTypeId3 { get; set; }
    public int CourseTypeId4 { get; set; }
    public User? User { get; set; }
    public CourseTypes? CourseType { get; set; }
    public ICollection<Thesis>? Theses { get; set; }

    public void Update(string name, bool isActive, int mentorId, int courseTypeId)
    {
        Name = name;
        IsActive = isActive;
        MentorId = mentorId;
        CourseTypeId = courseTypeId;
    }
}