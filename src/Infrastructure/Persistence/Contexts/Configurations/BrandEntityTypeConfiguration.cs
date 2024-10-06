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

            builder.ConfigureAuditable();

            builder.HasKey(m => m.Id);
            builder.ToTable("Brands");

            builder.HasData([
                new (){ Id = 1,Name="Fresh Meat", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 2,Name="Vegetables", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 3,Name="Fruit & Nut Gifts", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 4,Name="Fresh Berries", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 5,Name="Ocean Foods", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 6,Name="Butter & Eggs", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 7,Name="Fastfood", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 8,Name="Fresh Onion", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 9,Name="Papayaya & Crisps", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 10,Name="Oatmeal", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 11,Name="Fresh Bananas", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                ]);
        }
    }
}
