using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasIndex(e => e.Name).IsUnique();

            builder.HasData(
                new Product { Id = 1, Name = "Steel" },
                new Product { Id = 2, Name = "Wood Plank" },
                new Product { Id = 3, Name = "Stone" },
                new Product { Id = 4, Name = "Brick" },
                new Product { Id = 5, Name = "Cement" }
            );
        }
    }
}
