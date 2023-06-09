using FinalPaper.Domain.Utility;

namespace FinalPaper.Domain.Enums; 

public sealed class ThesisStatusTypes : Enumeration {
    public static ThesisStatusTypes PendingApproval = new(1, "Čeka odobrenje");
    public static ThesisStatusTypes Approved = new(2, "Odobreno");
    public static ThesisStatusTypes Rejected = new(3, "Odbijeno");

    public ThesisStatusTypes() {
        
    }

    public ThesisStatusTypes(int id, string name) : base(id, name)
    {
    }
}