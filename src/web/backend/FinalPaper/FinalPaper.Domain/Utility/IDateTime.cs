namespace FinalPaper.Domain.Utility; 

/// <summary>
///     DateTime wrapper interface that will allow us to transform dateTimes we are using
///     through out the application by adding differing concrete implementations if needed.
/// </summary>
public interface IDateTime {
    DateTime Now { get; }

    /// <summary>
    ///     Converts a date to a string.
    /// </summary>
    /// <param name="dateTime">Datetime to convert to string</param>
    /// <returns>string representation of the datetime</returns>
    string ToDateString(DateTime dateTime);
}