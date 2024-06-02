using Microsoft.EntityFrameworkCore;
using Ogani.WebUI.Models.Entities;

namespace Ogani.WebUI.Models.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            :base(options)
        {
                
        }

        public DbSet<ContactPost> ContactPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
