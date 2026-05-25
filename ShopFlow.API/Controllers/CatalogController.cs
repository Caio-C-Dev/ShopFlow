using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopFlow.Application.Catalog.Commands.CriarProduto;
using ShopFlow.Application.Catalog.Queries.GetProduto;

namespace ShopFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CriarProduto(
        CriarProdutoCommand command,
        CancellationToken ct)
    {
        var produtoId = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(CriarProduto), new { id = produtoId }, null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduto(Guid id, CancellationToken ct)
    {
        var produto = await _mediator.Send(new GetProdutoQuery(id), ct);

        if (produto is null)
            return NotFound();

        return Ok(produto);
    }
}