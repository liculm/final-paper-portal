namespace Api.Exceptions;

/// <summary>
///     Exception for when trying to access a resource with invalid credentials
/// </summary>
public sealed class NotAuthorizedException : InvalidOperationException
{
    public NotAuthorizedException()
    {
    }

    public NotAuthorizedException(string message) : base(message)
    {
    }

    public NotAuthorizedException(string message, Exception exception) : base(message, exception)
    {
    }
}