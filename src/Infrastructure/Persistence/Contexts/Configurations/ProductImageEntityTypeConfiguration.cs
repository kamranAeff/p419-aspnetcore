using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations
{
    class ProductImageEntityTypeConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.ProductId).HasColumnType("int").IsRequired();
            builder.Property(m => m.IsMain).HasColumnType("bit").IsRequired();
            builder.Property(m => m.Path).HasColumnType("varchar").HasMaxLength(50).IsRequired();

            builder.HasKey(m => m.Id);
            builder.ToTable("ProductImages");

            builder.HasOne<Product>()
                .WithMany()
                .HasPrincipalKey(m => m.Id)
                .HasForeignKey(m => m.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
