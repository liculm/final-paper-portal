using FinalPaper.Domain.Entities.Base;

namespace FinalPaper.Domain.Entities;

public class Thesis : Entity
{
    public Thesis()
    {
        Name = string.Empty;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsCurrent { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public User? User { get; set; }
    public Course? Course { get; set; }
    public ICollection<ThesisDefence>? ThesisDefences { get; set; }

    public void Update(string name, bool isCurrent, int courseId, int studentId)
    {
        Name = name;
        IsCurrent = isCurrent;
        CourseId = courseId;
        StudentId = studentId;
    }
}