using FinalPaper.Domain.Entities;
using MediatR;

namespace FinalPaper.Query.QueryHandlers.GetAllUsers;

public sealed record GetAllUsersQuery : IRequest<Unit>;

public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Unit>
{
    private readonly User user;

    public GetAllUsersQueryHandler(User user)
    {
        this.user = user;
    }


    public async Task<Unit> Handle(GetAllUsersQuery request, CancellationToken cancellationToken) {
        
        return Unit.Value;
    }
};