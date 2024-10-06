using Domain.Entities;
using Domain.Entities.Membership;
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
            builder.Property(m => m.Slug).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            builder.Property(m => m.Price).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
            builder.Property(m => m.IsDefault).HasColumnType("bit").IsRequired();

            builder.ConfigureAuditable();

            builder.HasIndex(m => new { m.ProductId, m.SizeId, m.ColorId }).IsUnique();
            builder.HasIndex(m => m.Slug).IsUnique();

            builder.HasKey(m => m.Id);
            builder.ToTable("ProductCards");

            builder.HasOne<Product>()
               .WithMany()
               .HasPrincipalKey(m => m.Id)
               .HasForeignKey(m => m.ProductId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Size>()
               .WithMany()
               .HasPrincipalKey(m => m.Id)
               .HasForeignKey(m => m.SizeId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Color>()
               .WithMany()
               .HasPrincipalKey(m => m.Id)
               .HasForeignKey(m => m.ColorId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
