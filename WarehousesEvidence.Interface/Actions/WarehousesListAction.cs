using WarehousesEvidence.Data.Repositories;

namespace WarehousesEvidence.Interface.Actions
{
    public class WarehousesListAction : IAction
    {
        private IWarehouseRepository _warehouseRepository;

        public WarehousesListAction(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public string Description => "Zabrazi sklady a jejich polozky";

        public async Task Show()
        {
            var warehouses = await _warehouseRepository.GetAllWithIncludes();
            foreach (var warehouse in warehouses)
            {
                Console.WriteLine($"\nNazev: {warehouse.Name}, Adresa: {warehouse.Address}");

                foreach (var product in warehouse.WarehouseProducts)
                    Console.WriteLine($"    Nazev: {product.Product.Name}, Mnozstvi: {product.Quantity}");
            }
        }
    }
}
