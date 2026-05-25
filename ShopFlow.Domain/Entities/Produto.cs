using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Domain.Entities
{
    public class Produto
    {
        private Produto() {
            Nome = null!;
            Descricao = null!;
            Preco = null!;
        }
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public Money Preco { get; private set; }
        public int EstoqueQuantidade { get; private set; }
        
        public Produto (string nome, string descricao, Money preco, int estoqueQuantidade)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome não pode ser vazio.");
            if (estoqueQuantidade < 0) throw new ArgumentOutOfRangeException("Estoque não pode ser negativo.");

            if (string.IsNullOrEmpty(descricao))
                throw new ArgumentException("Descrição não pode ser vazia.");

            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            EstoqueQuantidade = estoqueQuantidade;
            
        }

        public void DecrementarEstoque(int quantidade)
        {
            if (quantidade <= 0) throw new ArgumentOutOfRangeException("Quatidade deve ser maior que zero");
            if (quantidade > EstoqueQuantidade)
                throw new ArgumentOutOfRangeException("Quantidade superior ao estoque.");
            EstoqueQuantidade -= quantidade;
        }

        public void AtualizarPreco(Money novoPreco)
        {
            ArgumentNullException.ThrowIfNull(novoPreco);
            Preco = novoPreco;
        }
    }

}