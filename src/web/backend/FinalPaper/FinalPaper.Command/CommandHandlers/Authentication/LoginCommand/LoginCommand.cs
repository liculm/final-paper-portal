using FinalPaper.Domain.Exceptions;
using FinalPaper.Domain.Interfaces;
using FinalPaper.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Throw;

namespace FinalPaper.Command.CommandHandlers.Authentication.LoginCommand;

public sealed record LoginCommand(string Username, string Password, bool RememberMe) : IRequest<string>;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
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

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);

        user.ThrowIfNull("Username not found!");

        if (passwordHasher.VerifyHashedPassword(user.Password, request.Password) == PasswordVerificationResult.Failed)
            throw new NotAuthorizedException("Invalid user password!");

        user.Password = string.Empty;

        // TODO: [el] Add refresh token
        
        return jwtService.GenerateJwtToken("user", user);
    }
}