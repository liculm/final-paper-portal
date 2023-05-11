using FluentValidation;

namespace FinalPaper.Command.CommandHandlers.Document.GetPDFFile;

public sealed class GetPDFFileCommandValidator : AbstractValidator<GetPDFFileCommand> {
    public GetPDFFileCommandValidator() {
        RuleFor(x => x.fileName).NotEmpty();
    }
}