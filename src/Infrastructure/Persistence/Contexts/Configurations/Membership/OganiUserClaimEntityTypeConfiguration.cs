using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations.Membership
{
    class OganiUserClaimEntityTypeConfiguration : IEntityTypeConfiguration<OganiUserClaim>
    {
        public void Configure(EntityTypeBuilder<OganiUserClaim> builder)
        {
            builder.HasKey(m => m.Id);
            builder.ToTable("UserClaims", "Membership");
        }
    }
}
