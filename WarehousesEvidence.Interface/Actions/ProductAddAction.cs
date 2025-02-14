
using Sharprompt;
using WarehousesEvidence.App.Services;
using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Interface.Actions
{
    public class ProductAddAction : IAction
    {
        private IProductService _productService;

        public ProductAddAction(IProductService productService)
        {
            _productService = productService;
        }

        public string Description => "Vytvorit produkt";

        public async Task Show()
        {
            var name = Prompt.Input<string>("Zadejte nazev produktu");

            var newProduct = new Product
            {
                Name = name
            };

            try
            {
                var product = await _productService.Add(newProduct);
                Console.WriteLine($"\nProdukt {product.Name} byl vytvoren (Id: {product.ProductId}).");
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n{e.Message}");
            }
        }
    }
}
