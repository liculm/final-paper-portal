using FluentValidation;

namespace FinalPaper.Command.CommandHandlers.LoginCommand; 

public class LoginCommandValidator : AbstractValidator<LoginCommand> {
    public LoginCommandValidator() {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}