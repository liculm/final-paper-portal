using FinalPaper.Domain.ViewModels;
using FinalPaper.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalPaper.Query.QueryHandlers.GetAllMentors;

public sealed record GetAllMentorsQuery : IRequest<List<MentorViewModel>>;

public sealed class GetAllMentorsQueryHandler : IRequestHandler<GetAllMentorsQuery, List<MentorViewModel>> {
    private readonly FinalPaperDBContext context;

    public GetAllMentorsQueryHandler(FinalPaperDBContext context) {
        this.context = context;
    }

    public async Task<List<MentorViewModel>> Handle(GetAllMentorsQuery request, CancellationToken cancellationToken) {
        return await context.Users
            .AsNoTracking()
            .Where(x => x.IsActive && x.Role.Id == 2)
            .Select(x => new MentorViewModel(x.FirstName, x.LastName, 5))
            .ToListAsync(cancellationToken);
    }
};