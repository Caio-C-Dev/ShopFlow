using MediatR;

namespace ShopFlow.Application.Identity.Commands.Login;

public record LoginCommand(
    string Email,
    string Password) : IRequest<string>;