using Domain.Entities;
using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations
{
    class BasketEntityTypeConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.Property(m => m.UserId).HasColumnType("int");
            builder.Property(m => m.ProductCardId).HasColumnType("uniqueidentifier");
            builder.Property(m => m.Count).HasColumnType("int").IsRequired();
            builder.Property(m => m.CreatedAt).HasColumnType("datetime").IsRequired();

            builder.HasKey(m => new { m.UserId, m.ProductCardId });
            builder.ToTable("Baskets");

            builder.HasOne<ProductCard>()
                .WithMany()
                .HasPrincipalKey(m => m.Id)
                .HasForeignKey(m => m.ProductCardId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<OganiUser>()
                .WithMany()
                .HasPrincipalKey(m => m.Id)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
