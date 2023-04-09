using FinalPaper.Domain.Exceptions;
using FinalPaper.Domain.Interfaces;
using FinalPaper.Domain.ViewModels;
using FinalPaper.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Throw;

namespace FinalPaper.Command.CommandHandlers.Authentication.Login;

public sealed record LoginCommand(string Username, string Password, bool RememberMe) : IRequest<UserViewModel>;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, UserViewModel>
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

    public async Task<UserViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
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
            return new UserViewModel(user, jwtService.GenerateJwtToken(user));
        }

        if (user.RefreshToken is not null && user.RefreshToken.IsActive)
        {
            user.Password = string.Empty;
            return new UserViewModel(user, jwtService.GenerateJwtToken(user));
        }

        user.RefreshToken = jwtService.CreateRefreshToken();
        await context.SaveChangesAsync(cancellationToken);

        user.Password = string.Empty;
        return new UserViewModel(user, jwtService.GenerateJwtToken(user));
    }
}