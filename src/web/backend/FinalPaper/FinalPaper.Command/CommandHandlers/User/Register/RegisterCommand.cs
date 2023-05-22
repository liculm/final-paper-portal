using FinalPaper.Domain.Interfaces;
using FinalPaper.Domain.ViewModels;
using FinalPaper.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Throw;

namespace FinalPaper.Command.CommandHandlers.User.Register;

public sealed record RegisterCommand
    (string Username, string Password, string ConfirmPassword, int RoleId, string Firstname, string Lastname) : IRequest<UserViewModel>;

public sealed record RegisterCommandHandler : IRequestHandler<RegisterCommand, UserViewModel>
{
    private readonly FinalPaperDBContext context;
    private readonly IJwtService jwtService;
    private readonly IPasswordHasher passwordHasher;

    public RegisterCommandHandler(FinalPaperDBContext context, IPasswordHasher passwordHasher, IJwtService jwtService)
    {
        this.context = context;
        this.passwordHasher = passwordHasher;
        this.jwtService = jwtService;
    }

    public async Task<UserViewModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userExists = await context.Users.AsNoTracking()
            .AnyAsync(x => x.Username.ToLower() == request.Username.ToLower(),
                cancellationToken);

        userExists.Throw("Username already exists").IfTrue();

        var newUser = new Domain.Entities.User
        {
            Username = request.Username,
            Password = passwordHasher.HashPassword(request.Password),
            IsActive = true,
            RoleId = request.RoleId,
            FirstName = request.Firstname,
            LastName = request.Lastname
        };

        context.Users.Add(newUser);
        await context.SaveChangesAsync(cancellationToken);

        return new UserViewModel(newUser, jwtService.GenerateJwtToken(newUser));
    }
}