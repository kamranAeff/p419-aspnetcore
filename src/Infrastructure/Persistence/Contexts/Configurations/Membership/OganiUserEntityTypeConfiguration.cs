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
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.Name).HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(m => m.Surname).HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(m => m.UserName).HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(m => m.NormalizedUserName).HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(m => m.Email).HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(m => m.NormalizedEmail).HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(m => m.EmailConfirmed).HasColumnType("bit").IsRequired();
            builder.Property(m => m.PasswordHash).HasColumnType("varchar").HasMaxLength(400).IsRequired();
            builder.Property(m => m.SecurityStamp).HasColumnType("varchar").HasMaxLength(400).IsRequired();
            builder.Property(m => m.ConcurrencyStamp).HasColumnType("varchar").HasMaxLength(400).IsRequired();
            builder.Property(m => m.PhoneNumber).HasColumnType("varchar").HasMaxLength(40);
            builder.Property(m => m.PhoneNumberConfirmed).HasColumnType("bit").IsRequired();
            builder.Property(m => m.TwoFactorEnabled).HasColumnType("bit").IsRequired();
            builder.Property(m => m.LockoutEnd).HasColumnType("datetimeoffset");
            builder.Property(m => m.LockoutEnabled).HasColumnType("bit").IsRequired();
            builder.Property(m => m.AccessFailedCount).HasColumnType("int").IsRequired();
            builder.Property(m => m.IsSubscriber).HasColumnType("bit").IsRequired();
            builder.HasKey(m => m.Id);
            builder.ToTable("Users", "Membership");

            var userName = Environment.GetEnvironmentVariable("SUPERADMIN_USER");

            var password = Environment.GetEnvironmentVariable("SUPERADMIN_PASSWORD");
            var email = Environment.GetEnvironmentVariable("SUPERADMIN_EMAIL");

            var user = new OganiUser
            {
                Id = 1,
                Name = "Admin",
                Surname = "Admin",
                NormalizedUserName = userName.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
                ConcurrencyStamp = "a2679ce7-36e9-4db7-9bce-3b52995e5f2b",
                SecurityStamp = "a2679ce7-36e9-4db7-9bce-1152995e5f2b",
                IsSubscriber = false,
            };

            user.UserName = string.IsNullOrWhiteSpace(userName) ? $"{user.Name}.{user.Surname}".ToLower() : userName;
            user.PasswordHash = new PasswordHasher<OganiUser>().HashPassword(user, password);

            builder.HasData(user);
        }
    }
}
