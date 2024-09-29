using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations
{
    class BrandEntityTypeConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.Name).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();

            builder.HasKey(m => m.Id);
            builder.ToTable("Brands");

            builder.HasData([
                new (){ Id = 1,Name="Fresh Meat" },
                new (){ Id = 2,Name="Vegetables" },
                new (){ Id = 3,Name="Fruit & Nut Gifts" },
                new (){ Id = 4,Name="Fresh Berries" },
                new (){ Id = 5,Name="Ocean Foods" },
                new (){ Id = 6,Name="Butter & Eggs" },
                new (){ Id = 7,Name="Fastfood" },
                new (){ Id = 8,Name="Fresh Onion" },
                new (){ Id = 9,Name="Papayaya & Crisps" },
                new (){ Id = 10,Name="Oatmeal" },
                new (){ Id = 11,Name="Fresh Bananas" },
                ]);
        }
    }
}
