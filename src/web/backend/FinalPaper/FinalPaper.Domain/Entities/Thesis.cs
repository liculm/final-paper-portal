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
    public int ThesisStatusTypeId { get; set; }
    public int CourseId { get; set; }
    public Guid StudentId { get; set; }
    public User? User { get; set; }
    public Course? Course { get; set; }
    public ICollection<ThesisDefence>? ThesisDefences { get; set; }

    public void Update(string name, int thesisStatusTypeId, int courseId, Guid studentId)
    {
        Name = name;
        ThesisStatusTypeId = thesisStatusTypeId;
        CourseId = courseId;
        StudentId = studentId;
    }
}