using WarehousesEvidence.Data.Repositories;

namespace WarehousesEvidence.Interface.Actions
{
    public class WarehouseListAction : IAction
    {
        private IWarehouseRepository _warehouseRepository;

        public WarehouseListAction(IWarehouseRepository warehouseRepository)
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

                if (warehouse.WarehouseProducts is null)
                {
                    Console.WriteLine("    Sklad neobsahuje zadne produkty");
                    continue;

                }
                foreach (var product in warehouse.WarehouseProducts)
                    Console.WriteLine($"    Produkt: {product.Product.Name}, Mnozstvi: {product.Quantity}");
            }
        }
    }
}
