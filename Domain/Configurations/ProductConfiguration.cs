using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using technical_tests_backend_ssr.Models;

namespace technical_tests_backend_ssr.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("productdb");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(p => p.Name)
                .IsUnique();

            builder.Property(p => p.Price)
              .HasColumnType("decimal(18,2)") 
              .IsRequired();

            builder.HasIndex(p => p.Name)
              .IsUnique();
        }
    }
}


