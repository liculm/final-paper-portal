using FluentValidation;

namespace FinalPaper.Command.CommandHandlers.Thesis.ApproveThesis;

public sealed class SetIsApprovedCommandValidator : AbstractValidator<SetIsApprovedCommand> {
    public SetIsApprovedCommandValidator() {
        RuleFor(x => x.ThesisId).NotEmpty();
    }
}