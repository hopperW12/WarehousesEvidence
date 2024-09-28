
using Sharprompt;
using Sharprompt.Fluent;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Data.Services;

namespace WarehousesEvidence.Interface.Actions
{
    public class WarehouseRemove : IAction
    {
        private IWarehouseService _warehouseService;

        public WarehouseRemove(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        public string Description => "Vymazat sklad";

        public async Task Show()
        {
            var warehouses = await _warehouseService.GetAll();

            var selectWarehouse = Prompt
                .Select<Warehouse>(o => o
                    .WithMessage("Vyberte sklad, ktery chcete vymazat")
                    .WithItems(warehouses)
                    .WithTextSelector(wp => $"{wp.Name} | {wp.Address}"));

           
            var confirm = Prompt.Confirm("Opravdu chcete smazat tento sklad?");
            if (confirm)
            {
                await _warehouseService.DeleteWarehouse(selectWarehouse);
                Console.WriteLine("\nSklad byl uspesne smazan");
            }
            else
            {
                Console.WriteLine("\nSklad nebyl smazan");
            }
        }
    }
}
