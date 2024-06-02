using Microsoft.EntityFrameworkCore;
using ScrappingService.Models.Entities;

namespace ScrappingService.Models
{
    class DataContext : DbContext
    {
        internal Guid Id { get; }
        public DataContext(DbContextOptions options)
            : base(options)
        {
            this.Id = Guid.NewGuid();
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
