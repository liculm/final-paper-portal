using FinalPaper.Domain.Entities.Base;
using FinalPaper.Domain.Enums;

namespace FinalPaper.Domain.Entities;

public class Thesis : Entity
{
    public Thesis(int courseId, Guid studentId) {
        Name = "";
        ThesisStatusTypeId = 1;
        CourseId = courseId;
        StudentId = studentId;
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
    
    public void SetIsApprovedStatus(bool isApproved)
    {
        if (isApproved) {
            ThesisStatusTypeId = ThesisStatusTypes.Approved.Id;
        }
        else {
            ThesisStatusTypeId = ThesisStatusTypes.Rejected.Id;
        }
    }
}