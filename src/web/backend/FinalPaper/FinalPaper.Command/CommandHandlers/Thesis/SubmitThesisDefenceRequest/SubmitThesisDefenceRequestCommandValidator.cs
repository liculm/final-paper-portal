using FluentValidation;

namespace FinalPaper.Command.CommandHandlers.Thesis.SubmitThesisDefenceRequest;

public sealed class SubmitThesisDefenceRequestCommandValidator : AbstractValidator<SubmitThesisDefenceRequestCommand> {
    public SubmitThesisDefenceRequestCommandValidator() {
        RuleFor(x => x.thesisId).NotEmpty();
    }
}