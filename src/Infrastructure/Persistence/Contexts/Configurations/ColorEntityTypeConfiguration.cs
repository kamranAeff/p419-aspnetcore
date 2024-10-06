using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations
{
    class ColorEntityTypeConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.HexCode).HasColumnType("varchar").HasMaxLength(10).IsRequired();
            builder.Property(m => m.Name).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();

            builder.ConfigureAuditable();

            builder.HasKey(m => m.Id);
            builder.ToTable("Colors");

            builder.HasData([
                new (){ Id = 1,Name="Ag",HexCode="#ffffff", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 2,Name="Qara",HexCode="#000000", CreateBy=1, CreatedAt=new DateTime(2024,10,06) }
                ]);
        }
    }
}
