namespace FinalPaper.Query.QueryHandlers.GetAllMentors;

public sealed class MentorViewModel
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int AvailableNumberOfStudents { get; set; }
    public int TotalNumberOfStudents = 10;

    public MentorViewModel(string id, string firstName, string lastName, int availableNumberOfStudents)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        AvailableNumberOfStudents = availableNumberOfStudents;
    }
}

