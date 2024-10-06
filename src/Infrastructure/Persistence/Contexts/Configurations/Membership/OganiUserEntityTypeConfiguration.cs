using Domain.Entities.Membership;
using Microsoft.AspNetCore.Identity;
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

            var userName = Environment.GetEnvironmentVariable("SUPERADMIN_USER");

            var password = Environment.GetEnvironmentVariable("SUPERADMIN_PASSWORD");
            var email = Environment.GetEnvironmentVariable("SUPERADMIN_EMAIL");

            var user = new OganiUser
            {
                Id = 1,
                UserName = userName,
                NormalizedUserName = userName.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
                ConcurrencyStamp = "a2679ce7-36e9-4db7-9bce-3b52995e5f2b",
                SecurityStamp = "a2679ce7-36e9-4db7-9bce-1152995e5f2b"
            };

            user.PasswordHash = new PasswordHasher<OganiUser>().HashPassword(user, password);

            builder.HasData(user);
        }
    }
}
