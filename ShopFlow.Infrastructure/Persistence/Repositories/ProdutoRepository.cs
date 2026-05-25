using Microsoft.EntityFrameworkCore;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Interfaces;

namespace ShopFlow.Infrastructure.Persistence.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Produto produto, CancellationToken ct = default)
        {
            await _context.AddAsync(produto, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<Produto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Produtos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, ct);
            
        }

        public async Task<IReadOnlyCollection<Produto>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Produtos.AsNoTracking().ToListAsync(ct);
        }

        public async Task UpdateAsync(Produto produto, CancellationToken ct = default)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync(ct);
        }
    }        
}
