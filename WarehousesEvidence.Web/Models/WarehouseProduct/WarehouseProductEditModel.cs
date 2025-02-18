namespace WarehousesEvidence.Web.Models;

public class WarehouseProductEditModel
{
    public Data.Entities.Warehouse Warehouse { get; set; }
    public Data.Entities.Product Product { get; set; }

    public int Quantity { get; set; }
}