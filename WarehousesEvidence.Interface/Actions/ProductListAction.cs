
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

        public async Task<Result> Show()
        {
            Console.WriteLine("\nSeznam produktu:\n");
            foreach (var product in await _productRepository.GetAll())
                Console.WriteLine($"Id: {product.Id}, Nazev: {product.Name}");

            return Result.Ok();
        }
    }
}
