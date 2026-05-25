using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopFlow.Domain.Entities;

namespace ShopFlow.Infrastructure.Persistence.Configurations;

public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
{
    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.NomeProduto)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Quantidade)
            .IsRequired();

        builder.OwnsOne(i => i.PrecoUnitario, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("PrecoUnitario")
                .HasPrecision(18, 2)
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("Moeda")
                .HasMaxLength(3)
                .IsRequired();
        });
    }
}