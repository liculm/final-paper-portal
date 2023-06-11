using FinalPaper.Domain.Enums;
using FinalPaper.Infrastructure;
using MediatR;
using FinalPaper.Query.QueryHandlers.GetMentoringRequests;
using Microsoft.EntityFrameworkCore;

namespace FinalPaper.Query.QueryHandlers.GetMentoredStudents;

public sealed record GetMentoredStudentsQuery(Guid MentorId) : IRequest<List<MentoringRequestsViewModel>>;

public sealed record GetMentoredStudentsQueryHandler : IRequestHandler<GetMentoredStudentsQuery, List<MentoringRequestsViewModel>> {
    private readonly FinalPaperDBContext context;

    public GetMentoredStudentsQueryHandler(FinalPaperDBContext context) {
        this.context = context;
    }

    public async Task<List<MentoringRequestsViewModel>> Handle(GetMentoredStudentsQuery request, CancellationToken cancellationToken) {
        return await context.Thesis
            .AsNoTracking()
            .Where(x => x.Course.User.Id == request.MentorId &&
                        (x.ThesisStatusTypeId == ThesisStatusTypes.Approved.Id ||
                         x.ThesisStatusTypeId == ThesisStatusTypes.PendingDefence.Id))
            .Select(x => new MentoringRequestsViewModel(
                x.Id,
                x.User.FirstName,
                x.User.LastName,
                x.Name == "" ? "Nije odabrao temu" : x.Name,
                x.Course.Name,
                x.ThesisStatusTypeId == ThesisStatusTypes.PendingDefence.Id))
            .ToListAsync(cancellationToken);
    }
}