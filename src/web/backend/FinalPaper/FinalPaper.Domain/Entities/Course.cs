using System.ComponentModel.DataAnnotations;
using FinalPaper.Domain.Entities.Base;

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

    public void Update(string name, bool isActive, int mentorId, int courseTypeId)
    {
        Name = name;
        IsActive = isActive;
        MentorId = mentorId;
        CourseTypeId = courseTypeId;
    }
}