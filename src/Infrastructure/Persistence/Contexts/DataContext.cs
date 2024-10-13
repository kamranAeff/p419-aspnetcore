using Application.Services;
using Domain;
using Domain.Entities.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    //TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken
    class DataContext : IdentityDbContext<OganiUser, OganiRole, int, OganiUserClaim, OganiUserRole, OganiUserLogin, OganiRoleClaim, OganiUserToken>
    {
        private readonly IIdentityService identityService;

        public DataContext(DbContextOptions options, IIdentityService identityService)
            : base(options)
        {
            this.identityService = identityService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int userId = default;

            var changes = this.ChangeTracker.Entries<IAuditableEntity>();

            if (changes.Any())
                userId = identityService.UserId;

            foreach (var entry in changes)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow.AddHours(4);
                        entry.Entity.CreateBy = userId;
                        break;
                    case EntityState.Modified:
                        entry.Property(m => m.CreatedAt).IsModified = false;
                        entry.Property(m => m.CreateBy).IsModified = false;
                        entry.Entity.ModifiedAt = DateTime.UtcNow.AddHours(4);
                        entry.Entity.ModifiedBy = userId;
                        break;
                    case EntityState.Deleted:
                        entry.Property(m => m.CreatedAt).IsModified = false;
                        entry.Property(m => m.CreateBy).IsModified = false;
                        entry.Property(m => m.ModifiedAt).IsModified = false;
                        entry.Property(m => m.ModifiedBy).IsModified = false;
                        entry.Entity.DeletedAt = DateTime.UtcNow.AddHours(4);
                        entry.Entity.DeletedBy = userId;
                        entry.State = EntityState.Modified;
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
