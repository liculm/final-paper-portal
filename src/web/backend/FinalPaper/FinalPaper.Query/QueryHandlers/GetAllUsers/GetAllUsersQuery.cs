using FinalPaper.Domain.ViewModels;
using MediatR;
using FinalPaper.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FinalPaper.Query.QueryHandlers.GetAllUsers;

public sealed record GetAllUsersQuery : IRequest<List<UserBaseData>>;

public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserBaseData>>
{
    private readonly FinalPaperDBContext context;

    public GetAllUsersQueryHandler(FinalPaperDBContext context)
    {
        this.context = context;
    }

    public async Task<List<UserBaseData>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await context.Users
            .AsNoTracking()
            .Where(x => x.IsActive)
            .Select(x => new UserBaseData(x.Id.ToString(), x.FirstName, x.LastName, x.Username, x.Role.Id, x.Role.Name))
            .ToListAsync(cancellationToken);
    }
};