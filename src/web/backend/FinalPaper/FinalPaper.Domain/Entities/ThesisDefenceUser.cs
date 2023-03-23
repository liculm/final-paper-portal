using FinalPaper.Domain.Entities.Base;

namespace FinalPaper.Domain.Entities;

public class ThesisDefenceUser : Entity
{
    public int UserId { get; set; }
    public int ThesisDefenceId { get; set; }

    public void Update(int userId, int thesisDefenceId)
    {
        UserId = userId;
        ThesisDefenceId = thesisDefenceId;
    }
}