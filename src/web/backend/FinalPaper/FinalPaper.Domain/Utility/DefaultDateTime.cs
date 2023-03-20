namespace FinalPaper.Domain.Utility; 

public class DefaultDateTime : IDateTime{
    private const string dateFormat = "MM/dd/yyyy";
    private const string timeFormat = "h:mm tt";

    public DateTime Now => DateTime.UtcNow;

    public string ToDateString(DateTime dateTime)
    {
        return dateTime.ToString($"{dateFormat} {timeFormat}");
    }
}