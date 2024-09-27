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
                new Product { ProductId = 1, Name = "Steel" },
                new Product { ProductId = 2, Name = "Wood Plank" },
                new Product { ProductId = 3, Name = "Stone" },
                new Product { ProductId = 4, Name = "Brick" },
                new Product { ProductId = 5, Name = "Cement" }
            );
        }
    }
}
