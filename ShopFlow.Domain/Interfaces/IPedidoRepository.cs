using ShopFlow.Domain.Entities;

namespace ShopFlow.Domain.Interfaces;

public interface IPedidoRepository
{
    Task AddAsync(Pedido pedido, CancellationToken ct = default);
    Task <Pedido?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task <IReadOnlyCollection<Pedido>> GetAllByClienteIdAsync(Guid id, CancellationToken ct = default);
    Task UpdateAsync(Pedido pedido, CancellationToken ct = default);

}