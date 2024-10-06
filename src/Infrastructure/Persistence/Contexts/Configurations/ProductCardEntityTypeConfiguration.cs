using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations
{
    class ProductCardEntityTypeConfiguration : IEntityTypeConfiguration<ProductCard>
    {
        public void Configure(EntityTypeBuilder<ProductCard> builder)
        {
            builder.Property(m => m.Id).HasColumnType("uniqueidentifier");
            builder.Property(m => m.ProductId).HasColumnType("int").IsRequired();
            builder.Property(m => m.SizeId).HasColumnType("int").IsRequired();
            builder.Property(m => m.ColorId).HasColumnType("int").IsRequired();
            builder.Property(m => m.Price).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();

            builder.ConfigureAuditable();

            builder.HasIndex(m => new { m.ProductId, m.SizeId, m.ColorId }).IsUnique();

            builder.HasKey(m => m.Id);
            builder.ToTable("ProductCards");
        }
    }
}
