using FinalPaper.Command.CommandHandlers.Thesis.ApproveThesis;
using FluentValidation;

namespace FinalPaper.Command.CommandHandlers.Thesis.SelectThesisName;

public sealed class SelectThesisNameCommandValidator : AbstractValidator<SelectThesisNameCommand>
{
    public SelectThesisNameCommandValidator()
    {
        RuleFor(x => x.ThesisName).NotEmpty();
    }
}