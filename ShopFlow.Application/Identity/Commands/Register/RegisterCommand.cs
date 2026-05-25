using MediatR;

namespace ShopFlow.Application.Identity.Commands.Register;

public record RegisterCommand(
    string NomeCompleto,
    string Email,
    string Password) : IRequest<string>;