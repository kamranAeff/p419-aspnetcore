using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Persistence.Contexts.Configurations.Membership
{
    class OganiUserTokenEntityTypeConfiguration : IEntityTypeConfiguration<OganiUserToken>
    {
        public void Configure(EntityTypeBuilder<OganiUserToken> builder)
        {
            builder.Property(m => m.Type).HasColumnType("tinyint").IsRequired();
            builder.Property(m => m.ExpireDate).HasColumnType("smalldatetime").IsRequired();
            builder.Property(m => m.IsDisable).HasColumnType("bit").IsRequired();

            builder.HasKey(m => new { m.UserId, m.LoginProvider, m.Name, m.Type, m.ExpireDate });
            builder.ToTable("UserTokens", "Membership");
        }
    }
}
