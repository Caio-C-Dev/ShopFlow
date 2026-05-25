using MediatR;
using ShopFlow.Domain.Interfaces;

namespace ShopFlow.Application.Catalog.Queries.GetProduto;

public class GetProdutoQueryHandler
    : IRequestHandler<GetProdutoQuery, ProdutoResponse?>
{
    private readonly IProdutoRepository _produtoRepository;

    public GetProdutoQueryHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<ProdutoResponse?> Handle(
        GetProdutoQuery query,
        CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository
            .GetByIdAsync(query.Id, cancellationToken);

        if (produto is null)
            return null;

        return new ProdutoResponse(
            produto.Id,
            produto.Nome,
            produto.Descricao,
            produto.Preco.Amount,
            produto.Preco.Currency,
            produto.EstoqueQuantidade);
    }
}