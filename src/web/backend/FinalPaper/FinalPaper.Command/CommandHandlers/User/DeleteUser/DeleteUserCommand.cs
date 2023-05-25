using FinalPaper.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Throw;

namespace FinalPaper.Command.CommandHandlers.User.DeleteUser;

public sealed record DeleteUserCommand(Guid UserId) : IRequest<Unit>;

public sealed record DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit> {
    private readonly FinalPaperDBContext context;

    public DeleteUserCommandHandler(FinalPaperDBContext context) {
        this.context = context;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken) {
        var userToDelete = await context.Users
            .TagWithCallSite()
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        
        userToDelete.ThrowIfNull("User not found");
        
        userToDelete.MarkUserInactive();
        await context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}