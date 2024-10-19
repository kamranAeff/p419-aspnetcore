using Domain.Entities.Membership;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Contexts
{
    public static class DataContextAutoMigration
    {
        static public IApplicationBuilder AutoMigration(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DbContext>();
                db.Database.Migrate();  //update-database

                //SeedSuperAdminAccount(db);
            }
            return app;
        }

        private static void SeedSuperAdminAccount(DbContext db)
        {
            //var userName = Environment.GetEnvironmentVariable("SUPERADMIN_USER");

            //var user = db.Set<OganiUser>()
            //    .AsNoTracking()
            //    .FirstOrDefault(m => m.NormalizedUserName.Equals(userName.ToUpper()));

            //if (user is null)
            //{
            //    var password = Environment.GetEnvironmentVariable("SUPERADMIN_PASSWORD");
            //    var email = Environment.GetEnvironmentVariable("SUPERADMIN_EMAIL");

            //    user = new OganiUser
            //    {
            //        UserName = userName,
            //        NormalizedUserName = userName.ToUpper(),
            //        Email = email,
            //        NormalizedEmail = email.ToUpper(),
            //        EmailConfirmed = true,
            //        ConcurrencyStamp = Guid.NewGuid().ToString().ToLower(),
            //        SecurityStamp = Guid.NewGuid().ToString().ToLower()
            //    };

            //    user.PasswordHash = new PasswordHasher<OganiUser>().HashPassword(user, password);

            //    db.Add(user);
            //    db.SaveChanges();

            //    db.Set<OganiUserRole>().Add(new () { UserId = user.Id, RoleId = 1 });
            //    db.SaveChanges();
            //}
        }
    }
}
