using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations.Membership
{
    class OganiUserTokenEntityTypeConfiguration : IEntityTypeConfiguration<OganiUserToken>
    {
        public void Configure(EntityTypeBuilder<OganiUserToken> builder)
        {
            //builder.HasKey(m => m.Id);
            builder.ToTable("UserTokens", "Membership");
        }
    }
}
