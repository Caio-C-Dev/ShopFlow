using MediatR;

namespace ShopFlow.Application.Catalog.Commands.CriarProduto;

public record CriarProdutoCommand(
    string Nome, 
    string Descricao,
    decimal Preco,
    int Estoque,
    string Moeda 
    ) : IRequest<Guid>;
