namespace WarehousesEvidence.Data.Entities
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string Address { get; set; }
        public string Name { get; set; } 

        public ICollection<WarehouseProduct> WarehouseProducts { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
