namespace FinalPaper.Query.QueryHandlers.GetAllMentors;

public sealed class MentorViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int AvailableNumberOfStudents { get; set; }
    public int TotalNumberOfStudents = 10;

    public MentorViewModel(string firstName, string lastName, int availableNumberOfStudents)
    {
        FirstName = firstName;
        LastName = lastName;
        AvailableNumberOfStudents = availableNumberOfStudents;
    }
}

