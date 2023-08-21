using Microsoft.EntityFrameworkCore;

namespace MvcDemo.Entities
{
    public class DemoDbContext:DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {
        }

        public DbSet<DemoEntity> Demo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DemoEntity>().ToTable("Demo");
        }
    }
}
