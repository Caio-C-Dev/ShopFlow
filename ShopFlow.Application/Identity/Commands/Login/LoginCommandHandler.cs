using MediatR;
using ShopFlow.Application.Identity;

namespace ShopFlow.Application.Identity.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<string> Handle(LoginCommand command, CancellationToken ct)
    {
        return await _authService.LoginAsync(
            command.Email,
            command.Password);
    }
}