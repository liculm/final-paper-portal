using FluentValidation;

namespace FinalPaper.Command.CommandHandlers.Authentication.Register;

public sealed class RegisterCommandValidator : AbstractValidator<Register.RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Password).Equal(x => x.ConfirmPassword);
    }
}