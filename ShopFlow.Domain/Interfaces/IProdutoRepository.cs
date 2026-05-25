using ShopFlow.Domain.Entities;

namespace ShopFlow.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task AddAsync(Produto produto, CancellationToken ct = default);
        Task <Produto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task <IReadOnlyCollection<Produto>> GetAllAsync(CancellationToken ct = default);
        Task UpdateAsync(Produto produto, CancellationToken ct = default);

    }
}
