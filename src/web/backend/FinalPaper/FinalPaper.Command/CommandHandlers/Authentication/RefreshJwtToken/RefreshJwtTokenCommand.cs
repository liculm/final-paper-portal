using FinalPaper.Domain.Exceptions;
using FinalPaper.Domain.Interfaces;
using FinalPaper.Domain.ViewModels;
using FinalPaper.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Throw;

namespace FinalPaper.Command.CommandHandlers.Authentication.RefreshJwtToken;

public sealed record RefreshJwtTokenCommand(string? RefreshToken) : IRequest<UserViewModel>;

public sealed record RefreshJwtTokenCommandHandler : IRequestHandler<RefreshJwtTokenCommand, UserViewModel>
{
    private readonly FinalPaperDBContext context;
    private readonly IJwtService jwtService;

    public RefreshJwtTokenCommandHandler(FinalPaperDBContext context, IJwtService jwtService)
    {
        this.context = context;
        this.jwtService = jwtService;
    }

    public async Task<UserViewModel> Handle(RefreshJwtTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await context.RefreshToken.Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == request.RefreshToken, cancellationToken);
        
        refreshToken.ThrowIfNull("Refresh token not found!");
        refreshToken.User.ThrowIfNull("User not found!");

        if(!refreshToken.IsActive)
            throw new NotAuthorizedException("Refresh token is no longer active!");
        
        refreshToken.Update(jwtService.CreateRefreshToken()); 
        await context.SaveChangesAsync(cancellationToken);
        
        return new UserViewModel(refreshToken.User!, jwtService.GenerateJwtToken(refreshToken.User!));
    }
}