using FinalPaper.Domain.Interfaces;
using MediatR;

namespace FinalPaper.Command.CommandHandlers.LoginCommand;

public sealed record LoginCommand(string Username, string Password, bool RememberMe) : IRequest<string>;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IJwtService jwtService;

    public LoginCommandHandler(IJwtService jwtService)
    {
        this.jwtService = jwtService;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = request.Username + request.Password;
        
        throw new NotImplementedException();
    }
}