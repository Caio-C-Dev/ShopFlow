using MediatR;

namespace ShopFlow.Application.Catalog.Queries.GetProduto;

public record GetProdutoQuery(Guid Id) : IRequest<ProdutoResponse?>;