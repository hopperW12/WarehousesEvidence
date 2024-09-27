
using WarehousesEvidence.Data.Repositories;

namespace WarehousesEvidence.Interface.Actions
{
    public class ProductListAction : IAction
    {
        public string Description => "Vypise seznam produktu";

        private IProductRepository _productRepository;

        public ProductListAction(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Show()
        {
            Console.WriteLine("\nSeznam produktu:\n");
            foreach (var product in await _productRepository.GetAll())
                Console.WriteLine($"Id: {product.ProductId}, Nazev: {product.Name}");
        }
    }
}
