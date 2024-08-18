using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations
{
    class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.Title).HasColumnType("nvarchar").HasMaxLength(300).IsRequired();
            builder.Property(m => m.Slug).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            builder.Property(m => m.BrandId).HasColumnType("int").IsRequired();
            builder.Property(m => m.CategoryId).HasColumnType("int").IsRequired();
            builder.Property(m => m.Rate).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
            builder.Property(m => m.Weight).HasColumnType("decimal").HasPrecision(18, 3);
            builder.Property(m => m.UnitOfWeight).HasColumnType("tinyint").IsRequired();
            builder.Property(m => m.Description).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(m => m.Information).HasColumnType("nvarchar(max)").IsRequired();

            builder.HasIndex(m => m.Slug).IsUnique();

            builder.HasKey(m => m.Id);
            builder.ToTable("Products");


            builder.HasOne<Brand>()
                .WithMany()
                .HasPrincipalKey(m => m.Id)
                .HasForeignKey(m => m.BrandId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Category>()
                .WithMany()
                .HasPrincipalKey(m => m.Id)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
