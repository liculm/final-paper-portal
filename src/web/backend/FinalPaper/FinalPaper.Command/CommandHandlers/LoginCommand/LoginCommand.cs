using FinalPaper.Domain.Interfaces;
using MediatR;

namespace FinalPaper.Command.CommandHandlers.LoginCommand; 

public sealed record LoginCommand : IRequest<string> {
    public string Username { get; set; }
    public string Password { get; set; }
}

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string> {
    private readonly IJwtService jwtService;
    private readonly IRefreshTokenService refreshTokenService;

    public LoginCommandHandler(IJwtService jwtService, IRefreshTokenService refreshTokenService) {
        this.jwtService = jwtService;
        this.refreshTokenService = refreshTokenService;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken) {

        var result = request.Username + request.Password;

        
        
        
        return jwtService.GenerateAccessToken(2.ToString(), jwtService.GenerateRefreshToken()); 
    }
};