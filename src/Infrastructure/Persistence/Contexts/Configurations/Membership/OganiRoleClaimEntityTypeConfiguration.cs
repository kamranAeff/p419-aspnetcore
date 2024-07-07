using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations.Membership
{
    class OganiRoleClaimEntityTypeConfiguration : IEntityTypeConfiguration<OganiRoleClaim>
    {
        public void Configure(EntityTypeBuilder<OganiRoleClaim> builder)
        {
            builder.HasKey(m => m.Id);
            builder.ToTable("RoleClaims", "Membership");
        }
    }
}
