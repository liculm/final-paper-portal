using FluentValidation;

namespace FinalPaper.Command.CommandHandlers.Authentication.LoginCommand;

public sealed class LoginCommandValidator : AbstractValidator<Authentication.LoginCommand.LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}