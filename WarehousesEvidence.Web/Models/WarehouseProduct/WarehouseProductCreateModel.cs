using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Web.Models;

public class WarehouseProductCreateModel
{
    public Warehouse Warehouse { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }
    
    public ICollection<Product> AvailableProducts { get; set; }
}