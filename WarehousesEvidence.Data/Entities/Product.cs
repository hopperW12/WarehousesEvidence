 namespace WarehousesEvidence.Data.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public ICollection<WarehouseProduct> WarehouseProducts { get; set; }
        public ICollection<Warehouse> Warehouses { get; set; }
    }
}
