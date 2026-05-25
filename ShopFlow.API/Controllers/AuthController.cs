using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopFlow.Application.Identity.Commands.Login;
using ShopFlow.Application.Identity.Commands.Register;

namespace ShopFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command, CancellationToken ct)
    {
        var token = await _mediator.Send(command, ct);
        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command, CancellationToken ct)
    {
        var token = await _mediator.Send(command, ct);
        return Ok(new { token });
    }
}