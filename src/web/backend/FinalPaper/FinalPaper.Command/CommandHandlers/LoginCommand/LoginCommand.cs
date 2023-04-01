using MediatR;

namespace FinalPaper.Command.CommandHandlers.LoginCommand; 

public sealed record LoginCommand : IRequest<string> {
    public string Username { get; set; }
    public string Password { get; set; }
}

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string> {
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken) {

        var result = request.Username + request.Password;

        return result;
    }
};