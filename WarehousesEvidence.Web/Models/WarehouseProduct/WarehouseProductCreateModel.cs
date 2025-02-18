namespace WarehousesEvidence.Web.Models;

public class WarehouseProductCreateModel
{
    public Data.Entities.Warehouse Warehouse { get; set; }
    public Data.Entities.Product Product { get; set; }

    public int Quantity { get; set; }
    
    public ICollection<Data.Entities.Product> AvailableProducts { get; set; }
}