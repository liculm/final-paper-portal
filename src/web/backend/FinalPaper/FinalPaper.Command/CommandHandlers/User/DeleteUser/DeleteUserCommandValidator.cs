using FluentValidation;

namespace FinalPaper.Command.CommandHandlers.User.DeleteUser;

public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand> {
    public DeleteUserCommandValidator() {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Id is required");
    }
}