
using Sharprompt;
using Sharprompt.Fluent;
using System.Reflection;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Data.Services;

namespace WarehousesEvidence.Interface.Actions
{
    public class WarehouseEditAction : IAction
    {
        private IWarehouseService _warehouseService;
        private IProductService _productService;

        public WarehouseEditAction(IWarehouseService warehouseService, IProductService productService)
        {
            _warehouseService = warehouseService;
            _productService = productService;
        }

        public string Description => "Upravit sklad";

        public async Task Show()
        {
            var warehouses = await _warehouseService.GetAllWithIncludes();
            if (warehouses.Count == 0)
            {
                Console.WriteLine("\nNeexistuje zadny sklad");
                return;
            }

            var selectWarehouse = Prompt
                .Select<Warehouse>(o => 
                    o.WithMessage("Vyber sklad")
                     .WithItems(warehouses)
                     .WithTextSelector(e => $"{e.Name} | {e.Address}"));
            var action = Prompt
                .Select<WarehouseEditMenuAction>(o => 
                    o.WithMessage("Vyber akci")
                     .WithItems(WarehouseEditMenuAction.Values())
                     .WithTextSelector(e => e.Name));

            if (action == WarehouseEditMenuAction.EditProduct || action == WarehouseEditMenuAction.RemoveProduct) 
            {
                var product = Prompt
                    .Select<WarehouseProduct>(o => 
                        o.WithMessage("Vyber produkt")
                         .WithItems(selectWarehouse.WarehouseProducts)
                         .WithTextSelector(e => $"{e.Product.Name} - {e.Quantity}"));

                if (action == WarehouseEditMenuAction.EditProduct)
                {
                    var value = Prompt.Input<int>("Upravit Mnozstvi");

                    product.Quantity = value;

                    await _warehouseService.UpdateWarehouse(selectWarehouse);
                    Console.WriteLine($"\nMnozstvi bylo aktualizovano");
                }
                else 
                {
                    selectWarehouse.WarehouseProducts.Remove(product);
                    await _warehouseService.UpdateWarehouse(selectWarehouse);
                    Console.WriteLine($"\nProdukt byl odebran");
                }
            } 
            else if (action == WarehouseEditMenuAction.AddProduct)
            {
                var products = await _productService.GetAll();
                var product = Prompt
                    .Select<Product>(o =>
                        o.WithMessage("Vyber produkt")
                         .WithItems(products.Except(selectWarehouse.Products))
                         .WithTextSelector(e => e.Name));

                var quantity = Prompt.Input<int>("Mnozstvi");

                selectWarehouse.WarehouseProducts.Add(new WarehouseProduct
                {
                    Product = product,
                    Quantity = quantity
                });

                await _warehouseService.UpdateWarehouse(selectWarehouse);
                Console.WriteLine($"\nProdukt byl pridan");
            }
            else if (action == WarehouseEditMenuAction.EditName) 
            {
                var newName = Prompt.Input<string>("Nove jmeno");
                selectWarehouse.Name = newName;

                await _warehouseService.UpdateWarehouse(selectWarehouse);
                Console.WriteLine($"\nJmeno bylo aktualizovano");
            }
            else if (action == WarehouseEditMenuAction.EditAddress)
            {
                var newAddress = Prompt.Input<string>("Nova adresa");
                selectWarehouse.Address = newAddress;

                await _warehouseService.UpdateWarehouse(selectWarehouse);
                Console.WriteLine($"\nAdresa bylo aktualizovano");
            }
        }
    }

    public class WarehouseEditMenuAction
    {
        public static WarehouseEditMenuAction AddProduct = new WarehouseEditMenuAction("Pridat produkt");
        public static WarehouseEditMenuAction EditProduct = new WarehouseEditMenuAction("Upravit mnozstvi");
        public static WarehouseEditMenuAction RemoveProduct = new WarehouseEditMenuAction("Odebrat produkt");
        public static WarehouseEditMenuAction EditName = new WarehouseEditMenuAction("Upravit jmeno");
        public static WarehouseEditMenuAction EditAddress = new WarehouseEditMenuAction("Upravit adresu");

        public string Name { get; private set; }

        private WarehouseEditMenuAction(string name)
        {
            Name = name;
        }

        public static IEnumerable<WarehouseEditMenuAction> Values()
        {
            var fields = typeof(WarehouseEditMenuAction).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            foreach (var field in fields)
                yield return (WarehouseEditMenuAction) field.GetValue(null);
        }
    }
}
