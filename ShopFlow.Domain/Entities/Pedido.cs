using ShopFlow.Domain.Enums;

namespace ShopFlow.Domain.Entities
{
    public class Pedido
    {
        private Pedido() { } 
        public Guid Id { get; private set; }
        public Guid ClienteId {  get; private set; }
        public DateTime CriadoEm {  get; private set; }
        public StatusPedido Status {  get; private set; }

        private readonly List<ItemPedido> _itens = new();
        public IReadOnlyCollection<ItemPedido> Itens => _itens.AsReadOnly();
        public Pedido(Guid clienteId) 
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
            CriadoEm = DateTime.UtcNow;
            Status = StatusPedido.Pendente;
        }

        public void AdicionarItem(ItemPedido item)
        {
            ArgumentNullException.ThrowIfNull(item);
            _itens.Add(item);
        }

        public void Concluir()
        {
            if (!_itens.Any())
                throw new InvalidOperationException("Pedido não pode ser concluido sem itens.");
            Status = StatusPedido.Confirmado;
        }

    }
}
