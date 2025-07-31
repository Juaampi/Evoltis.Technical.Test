using Microsoft.EntityFrameworkCore;
using technical_tests_backend_ssr.Configurations;
using technical_tests_backend_ssr.Models;

namespace technical_tests_backend_ssr.Domain
{
    public class EvoltisTestDbContext : DbContext
    {
        public EvoltisTestDbContext(DbContextOptions<EvoltisTestDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());

        }
    }
}
