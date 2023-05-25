using MediatR;
using FinalPaper.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Throw;

namespace FinalPaper.Command.CommandHandlers.User.UpdateUser;

public sealed record UpdateUserCommand(Guid Id, string FirstName, string LastName, string Username, int RoleId) : IRequest<Unit>;

public sealed record UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit> {
    private readonly FinalPaperDBContext context;

    public UpdateUserCommandHandler(FinalPaperDBContext context) {
        this.context = context;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken) {
        var userToUpdate = await context.Users
            .TagWithCallSite()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        userToUpdate.ThrowIfNull("User not found");
        
        userToUpdate.UpdateUserInfo(request.FirstName, request.LastName, request.Username, request.RoleId);
        await context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}