namespace ShopFlow.Application.Catalog.Queries.GetProduto;

public record ProdutoResponse(
    Guid Id,
    string Nome,
    string Descricao,
    decimal Preco,
    string Moeda,
    int Estoque);