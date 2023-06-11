using FluentValidation;

namespace FinalPaper.Query.QueryHandlers.GetMentoringRequests;

public sealed class GetMentoringRequestsQueryValidator : AbstractValidator<GetMentoringRequestsQuery> {
    public GetMentoringRequestsQueryValidator() {
        RuleFor(x => x.MentorId).NotEmpty();
    }
}