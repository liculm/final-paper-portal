using FinalPaper.Domain.Enums;
using FinalPaper.Domain.Exceptions;
using FinalPaper.Domain.Interfaces;
using FinalPaper.Domain.Utility;
using FinalPaper.Domain.ViewModels;
using FinalPaper.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Throw;

namespace FinalPaper.Command.CommandHandlers.User.Login;

public sealed record LoginCommand(string Username, string Password, bool RememberMe) : IRequest<UserData>;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, UserData>
{
    private readonly FinalPaperDBContext context;
    private readonly IJwtService jwtService;
    private readonly IPasswordHasher passwordHasher;

    public LoginCommandHandler(IJwtService jwtService, FinalPaperDBContext context, IPasswordHasher passwordHasher)
    {
        this.jwtService = jwtService;
        this.context = context;
        this.passwordHasher = passwordHasher;
    }

    public async Task<UserData> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.Include(x => x.RefreshToken)
            .FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);

        user.ThrowIfNull("Username not found!");

        if (passwordHasher.VerifyHashedPassword(user.Password, request.Password) == PasswordVerificationResult.Failed)
            throw new NotAuthorizedException("Invalid user password!");

        if (!request.RememberMe)
        {
            user.RefreshToken?.Revoke();
            user.Password = string.Empty;
            return new UserData( 
                jwtService.GenerateJwtToken(user),
                user.FirstName,
                user.LastName,
                user.Username,
                user.RoleId,
                user.RefreshToken,
                Enumeration.FromValue<Roles>(user.RoleId).Name
            );
        }

        if (user.RefreshToken is not null && user.RefreshToken.IsActive)
        {
            user.Password = string.Empty;
            return new UserData( 
                jwtService.GenerateJwtToken(user),
                user.FirstName,
                user.LastName,
                user.Username,
                user.RoleId,
                user.RefreshToken,
                Enumeration.FromValue<Roles>(user.RoleId).Name
            );
        }

        user.RefreshToken = jwtService.CreateRefreshToken();
        await context.SaveChangesAsync(cancellationToken);

        user.Password = string.Empty;
        return new UserData( 
            jwtService.GenerateJwtToken(user),
            user.FirstName,
            user.LastName,
            user.Username,
            user.RoleId,
            user.RefreshToken,
            Enumeration.FromValue<Roles>(user.RoleId).Name
                );
    }
}