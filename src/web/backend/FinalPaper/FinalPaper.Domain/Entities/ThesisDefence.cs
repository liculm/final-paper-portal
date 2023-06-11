using FinalPaper.Domain.Entities.Base;

namespace FinalPaper.Domain.Entities;

public class ThesisDefence : Entity
{
    public ThesisDefence()
    {
        Room = string.Empty;
    }

    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public string? Room { get; set; }
    public int? DefenceScore { get; set; }
    public int? FinalPaperScore { get; set; }
    public int ThesesId { get; set; }
    public Thesis? Thesis { get; set; }
    public ICollection<ThesisDefenceUser>? ThesesDefenceUsers { get; set; }

    public void Update(DateTime date, string room, int defenceScore,
        int finalPaperScore, int thesesId)
    {
        Date = date;
        Room = room;
        DefenceScore = defenceScore;
        FinalPaperScore = finalPaperScore;
        ThesesId = thesesId;
    }

    public  ThesisDefence(int thesesId)
    {
        ThesesId = thesesId;
    }
}