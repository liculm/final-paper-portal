using FluentValidation;

namespace FinalPaper.Command.CommandHandlers.Authentication.RegisterCommand;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Password).Equal(x => x.ConfirmPassword);
    }
}