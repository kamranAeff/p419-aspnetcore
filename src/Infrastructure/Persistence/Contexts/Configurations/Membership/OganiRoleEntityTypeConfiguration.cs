using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations.Membership
{
    class OganiRoleEntityTypeConfiguration : IEntityTypeConfiguration<OganiRole>
    {
        public void Configure(EntityTypeBuilder<OganiRole> builder)
        {
            builder.HasKey(m => m.Id);
            builder.ToTable("Roles", "Membership");
        }
    }
}
