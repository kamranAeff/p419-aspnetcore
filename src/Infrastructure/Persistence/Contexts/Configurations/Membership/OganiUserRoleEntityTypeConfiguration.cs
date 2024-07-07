using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations.Membership
{
    class OganiUserRoleEntityTypeConfiguration : IEntityTypeConfiguration<OganiUserRole>
    {
        public void Configure(EntityTypeBuilder<OganiUserRole> builder)
        {
            builder.HasKey(m => new { m.UserId, m.RoleId });
            builder.ToTable("UserRoles", "Membership");
        }
    }
}
