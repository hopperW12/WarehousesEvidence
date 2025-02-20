
using Sharprompt;
using WarehousesEvidence.App.Services;
using WarehousesEvidence.Data.Entities;

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

        public async Task<Result> Show()
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
            
            return Result.Ok();
        }
    }
}
