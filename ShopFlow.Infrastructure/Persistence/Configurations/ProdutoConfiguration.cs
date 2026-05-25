using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopFlow.Domain.Entities;

namespace ShopFlow.Infrastructure.Persistence.Configurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasMaxLength(1000);

        builder.OwnsOne(p => p.Preco, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("Preco")
                .HasPrecision(18, 2)
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("Moeda")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.Property(p => p.EstoqueQuantidade)
            .IsRequired();
    }
}