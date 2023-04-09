using FinalPaper.Domain.Entities.Base;

namespace FinalPaper.Domain.Entities;

public class ThesisDefenceUser : Entity
{
    public Guid UserId { get; set; }
    public int ThesisDefenceId { get; set; }
    public User? User { get; set; }
    public ThesisDefence? ThesisDefence { get; set; }

    public void Update(Guid userId, int thesisDefenceId)
    {
        UserId = userId;
        ThesisDefenceId = thesisDefenceId;
    }
}