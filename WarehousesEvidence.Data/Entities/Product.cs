using Newtonsoft.Json;

namespace WarehousesEvidence.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<WarehouseProduct> WarehouseProducts { get; set; }
        [JsonIgnore]
        public ICollection<Warehouse> Warehouses { get; set; }
    }
}
