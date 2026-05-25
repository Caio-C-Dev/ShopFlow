using MediatR;
using ShopFlow.Application.Identity;

namespace ShopFlow.Application.Identity.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<string> Handle(RegisterCommand command, CancellationToken ct)
    {
        return await _authService.RegisterAsync(
            command.NomeCompleto,
            command.Email,
            command.Password);
    }
}