
using Sharprompt;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Data.Services;

namespace WarehousesEvidence.Interface.Actions
{
    public class WarehouseAddAction : IAction
    {
        private IWarehouseService _warehouseService;

        public WarehouseAddAction(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        public string Description => "Pridat sklad";

        public async Task Show()
        {
            var name = Prompt.Input<string>("Zadejte nazev skladu");
            var address = Prompt.Input<string>("Zadejte nazev adresu");

            var warehouse = new Warehouse
            {
                Name = name,
                Address = address
            };  

            await _warehouseService.AddWarehouse(warehouse);

            Console.WriteLine("\nSklad byl uspesne pridan");
        }
    }
}
