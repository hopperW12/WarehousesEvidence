using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Data.Configurations
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouses");

            builder.HasMany(wp => wp.Products)
                .WithMany(p => p.Warehouses)
                .UsingEntity<WarehouseProduct>(
                    j => j.HasOne(wp => wp.Product)
                        .WithMany(p => p.WarehouseProducts)
                        .HasForeignKey(wp => wp.ProductId),
                    j => j.HasOne(wp => wp.Warehouse)
                        .WithMany(w => w.WarehouseProducts)
                        .HasForeignKey(wp => wp.WarehouseId)
                );

            builder.HasData(
                new Warehouse { WarehouseId = 1, Name = "Building - Warehouse", Address = "18455 S Figueroa St" },
                new Warehouse { WarehouseId = 2, Name = "Main Warehouse", Address = "Gardena CA 90248-4503" }
            );
        }
    }
}
