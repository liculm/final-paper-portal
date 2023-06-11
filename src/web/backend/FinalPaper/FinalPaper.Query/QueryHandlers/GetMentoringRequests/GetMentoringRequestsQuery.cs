using FinalPaper.Domain.Enums;
using MediatR;
using FinalPaper.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FinalPaper.Query.QueryHandlers.GetMentoringRequests;

public sealed record GetMentoringRequestsQuery(Guid MentorId) : IRequest<List<MentoringRequestsViewModel>>;

public sealed record GetMentoringRequestsQueryHandler : IRequestHandler<GetMentoringRequestsQuery, List<MentoringRequestsViewModel>> {
    private readonly FinalPaperDBContext context;

    public GetMentoringRequestsQueryHandler(FinalPaperDBContext context) {
        this.context = context;
    }

    public async Task<List<MentoringRequestsViewModel>> Handle(GetMentoringRequestsQuery request, CancellationToken cancellationToken) {
        return await context.Thesis
            .AsNoTracking()
            .Where(x => x.Course.User.Id == request.MentorId &&
                        x.ThesisStatusTypeId == ThesisStatusTypes.PendingApproval.Id)
            .Select(x => new MentoringRequestsViewModel(
                x.Id,
                x.User.FirstName,
                x.User.LastName,
                x.Name == "" ? "Nije odabrao temu" : x.Name,
                x.Course.Name))
            .ToListAsync(cancellationToken);
    }
}