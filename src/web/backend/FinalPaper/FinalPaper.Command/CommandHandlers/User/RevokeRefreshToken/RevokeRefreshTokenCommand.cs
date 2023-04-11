using FinalPaper.Domain.Interfaces;
using FinalPaper.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalPaper.Command.CommandHandlers.User.RevokeRefreshToken;

public sealed record RevokeRefreshTokenCommand(string? RefreshToken) : IRequest<bool>;

public sealed record RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, bool>
{
    private readonly FinalPaperDBContext context;
    private readonly IJwtService jwtService;

    public RevokeRefreshTokenCommandHandler(FinalPaperDBContext context, IJwtService jwtService)
    {
        this.context = context;
        this.jwtService = jwtService;
    }

    public async Task<bool> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken =
            await context.RefreshToken.FirstOrDefaultAsync(x => x.Token == request.RefreshToken, cancellationToken);

        if (refreshToken is null || !refreshToken.IsActive)
            return false;

        refreshToken.Revoke();
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}