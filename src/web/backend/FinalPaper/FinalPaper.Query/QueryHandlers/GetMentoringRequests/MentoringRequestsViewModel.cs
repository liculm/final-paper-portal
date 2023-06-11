namespace FinalPaper.Query.QueryHandlers.GetMentoringRequests;

public sealed class MentoringRequestsViewModel {
    public int thesisId { get; set; }
    public string studentFirstName { get; set; }
    public string studentLastName { get; set; }
    public string thesisName { get; set; }
    public string courseName { get; set; }

    public MentoringRequestsViewModel(int thesisId, string studentFirstName, string studentLastName, string thesisName, string courseName) {
        this.thesisId = thesisId;
        this.studentFirstName = studentFirstName;
        this.studentLastName = studentLastName;
        this.thesisName = thesisName;
        this.courseName = courseName;
    }
};