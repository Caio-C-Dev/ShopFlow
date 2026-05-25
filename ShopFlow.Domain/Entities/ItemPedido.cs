using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Domain.Entities
{
    public class ItemPedido
    {
        private ItemPedido() {
            NomeProduto = null!;
            PrecoUnitario = null!;
        }
        public Guid Id { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string NomeProduto { get; private set; }
        public Money PrecoUnitario { get; private set; }
        public int Quantidade { get; private set; }
        public decimal Total => PrecoUnitario.Amount * Quantidade;

        public ItemPedido(Guid produtoId, string nomeProduto, Money precoUnitario, int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser positiva.");
            if (produtoId == Guid.Empty)
                throw new ArgumentException("ProdutoId inválido");

            Id = Guid.NewGuid();
            ProdutoId = produtoId;
            NomeProduto = nomeProduto;
            PrecoUnitario = precoUnitario;
            Quantidade = quantidade;
        }
    }
}
