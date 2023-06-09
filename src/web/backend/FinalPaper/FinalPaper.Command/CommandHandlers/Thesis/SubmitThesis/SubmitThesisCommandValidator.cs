using FluentValidation;

namespace FinalPaper.Command.CommandHandlers.Thesis.SubmitThesis;

public sealed class SubmitThesisCommandValidator : AbstractValidator<SubmitThesisCommand> {
    public SubmitThesisCommandValidator() {
        RuleFor(x => x.courseId).NotEmpty();
        RuleFor(x => x.studentId).NotEmpty();
    }
}