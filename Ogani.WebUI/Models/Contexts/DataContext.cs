using Microsoft.EntityFrameworkCore;
using Ogani.WebUI.Models.Entities;

namespace Ogani.WebUI.Models.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<ContactPost> ContactPosts { get; set; }
        public DbSet<Subscribe> Subscribers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public override int SaveChanges()
        {
            #warning OVERRIDE SaveChanges
            return base.SaveChanges();
        }
    }
}
