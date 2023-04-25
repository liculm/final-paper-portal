using FluentValidation;

namespace FinalPaper.Command.CommandHandlers.User.Login;

public sealed class LoginCommandValidator : AbstractValidator<Login.LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}