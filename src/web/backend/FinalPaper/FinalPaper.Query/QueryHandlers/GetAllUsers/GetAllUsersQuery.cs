using MediatR;

namespace FinalPaper.Query.QueryHandlers.GetAllUsers; 

public sealed record GetAllUsersQuery : IRequest<string> {
    public string Username { get; set; }
    public string Password { get; set; }
}

public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, string> {
    public async Task<string> Handle(GetAllUsersQuery request, CancellationToken cancellationToken) {

        var result = request.Username + request.Password;

        return result;
    }
};