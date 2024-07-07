using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations.Membership
{
    class OganiUserEntityTypeConfiguration : IEntityTypeConfiguration<OganiUser>
    {
        public void Configure(EntityTypeBuilder<OganiUser> builder)
        {
            builder.HasKey(m => m.Id);
            builder.ToTable("Users", "Membership");
        }
    }
}
