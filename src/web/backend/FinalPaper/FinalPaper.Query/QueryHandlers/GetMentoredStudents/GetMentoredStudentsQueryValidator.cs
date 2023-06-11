using FluentValidation;

namespace FinalPaper.Query.QueryHandlers.GetMentoredStudents;

public sealed class GetMentoredStudentsQueryValidator : AbstractValidator<GetMentoredStudentsQuery> {
    public GetMentoredStudentsQueryValidator() {
        RuleFor(x => x.MentorId).NotEmpty();
    }
}