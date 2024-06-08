using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ogani.WebUI.Models.Entities;

namespace Ogani.WebUI.Models.Contexts.Configurations
{
    class SubscribeEntityTypeConfiguration : IEntityTypeConfiguration<Subscribe>
    {
        public void Configure(EntityTypeBuilder<Subscribe> builder)
        {
            builder.Property(m => m.Email).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(m => m.CreatedAt).HasColumnType("datetime").IsRequired();
            builder.Property(m => m.ApprovedAt).HasColumnType("datetime");

            builder.HasKey(m => m.Email);
            builder.ToTable("Subscribers");
        }
    }
}
