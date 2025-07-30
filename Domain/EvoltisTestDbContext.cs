using Microsoft.EntityFrameworkCore;
using technical_tests_backend_ssr.Configurations;
using technical_tests_backend_ssr.Models;

namespace technical_tests_backend_ssr.Domain
{
    public class EvoltisTestDbContext : DbContext
    {
        public EvoltisTestDbContext(DbContextOptions<EvoltisTestDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "server=192.168.1.155;port=3306;database=productdb;user=dotnet_user;password=tu_contraseña;",
                    new MySqlServerVersion(new Version(10, 1, 38))
                );
            }
        }

        public DbSet<Product> Products { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());

        }
    }
}
