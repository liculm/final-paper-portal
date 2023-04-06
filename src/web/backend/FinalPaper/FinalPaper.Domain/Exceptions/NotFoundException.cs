namespace FinalPaper.Domain.Exceptions;

/// <summary>
///     Exception for a resource is not found.
/// </summary>
public sealed class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception exception) : base(message, exception)
    {
    }
}