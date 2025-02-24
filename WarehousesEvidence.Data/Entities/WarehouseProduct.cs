using Newtonsoft.Json;

namespace WarehousesEvidence.Data.Entities
{
    public class WarehouseProduct
    {
        public int WarehouseId { get; set; }
        [JsonIgnore]
        public Warehouse Warehouse { get; set; }

        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
