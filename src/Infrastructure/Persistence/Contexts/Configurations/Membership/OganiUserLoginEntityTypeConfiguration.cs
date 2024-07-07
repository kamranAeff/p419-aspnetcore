using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations.Membership
{
    class OganiUserLoginEntityTypeConfiguration : IEntityTypeConfiguration<OganiUserLogin>
    {
        public void Configure(EntityTypeBuilder<OganiUserLogin> builder)
        {
            //builder.HasKey(m => m.Id);
            builder.ToTable("UserLogins", "Membership");
        }
    }
}
