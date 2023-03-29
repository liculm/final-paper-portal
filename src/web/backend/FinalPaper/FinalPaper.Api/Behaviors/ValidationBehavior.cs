using System.Text;
using FluentValidation;
using MediatR;

namespace Api.Behaviors;

/// <summary>
///     Behavior used by the MediatR pipeline which intercepts and validates Commands
///     that are send via MediatR.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> {
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> logger;
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidationBehavior<TRequest, TResponse>> logger) {
        this.validators = validators;
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next) {
        var typeName = request.GetGenericTypeName();

        logger.LogInformation("Validating command {CommandType}", typeName);

        var failures = validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any()) {
            logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}",
                typeName, request, failures);

            var errorMessage = new StringBuilder($"Command Validation Errors for type {typeof(TRequest).Name}:");
            foreach (var failure in failures) {
                errorMessage.Append($" {failure.ErrorMessage}");
            }

            throw new ValidationException(errorMessage.ToString());
        }

        return await next();
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}

/// <summary>
///     Utility class for working with Type Names.
/// </summary>
public static class GenericTypeExtensions {
    /// <summary>
    ///     Return Generic Type Name for the given Type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns>Type Name as string</returns>
    public static string GetGenericTypeName(this Type type) {
        var typeName = string.Empty;

        if (type.IsGenericType) {
            var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
            typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
        }
        else {
            typeName = type.Name;
        }

        return typeName;
    }

    /// <summary>
    ///     Return Generic Type Name for the given object.
    /// </summary>
    /// <param name="object"></param>
    /// <returns>Type Name as string</returns>
    public static string GetGenericTypeName(this object @object) {
        return @object.GetType().GetGenericTypeName();
    }
}