using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Data.Configurations
{
    public class WarehouseProductConfiguration : IEntityTypeConfiguration<WarehouseProduct>
    {
        public void Configure(EntityTypeBuilder<WarehouseProduct> builder)
        {
            builder.ToTable("WarehouseProducts");

            builder.HasKey(wp => new { wp.WarehouseId, wp.ProductId });

            builder.HasData(
                new WarehouseProduct { ProductId = 1, WarehouseId = 1, Quantity = 52 },
                new WarehouseProduct { ProductId = 2, WarehouseId = 1, Quantity = 12 },
                new WarehouseProduct { ProductId = 3, WarehouseId = 1, Quantity = 5 },
                new WarehouseProduct { ProductId = 4, WarehouseId = 2, Quantity = 8 },
                new WarehouseProduct { ProductId = 5, WarehouseId = 2, Quantity = 3 }
            );
        }
    }
}
