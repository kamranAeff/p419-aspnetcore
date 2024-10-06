using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations
{
    class SizeEntityTypeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.Name).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            builder.Property(m => m.SmallName).HasColumnType("varchar").HasMaxLength(5).IsRequired();

            builder.ConfigureAuditable();

            builder.HasKey(m => m.Id);
            builder.ToTable("Sizes");

            builder.HasData([
                new (){ Id = 1,Name="Extra small",SmallName="XS", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 2,Name="Small",SmallName="S", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 3,Name="Medium",SmallName="M", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 4,Name="Large",SmallName="L", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 5,Name="Extra large",SmallName="XL", CreateBy=1, CreatedAt=new DateTime(2024,10,06) }
                ]);
        }
    }
}
