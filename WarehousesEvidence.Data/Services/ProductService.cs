using Microsoft.EntityFrameworkCore;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Data.Repositories;

namespace WarehousesEvidence.Data.Services
{
    public interface IProductService : IService
    {
        Task<Product?> GetById(int id);
        Task<Product> Add(Product product);
        Task<Product> Update(Product product);
        Task Remove(Product product);
        Task<ICollection<Product>> GetAll();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IAuditRepository _auditRepository;

        public ProductService(IProductRepository productRepository, IAuditRepository auditRepository)
        {
            _productRepository = productRepository;
            _auditRepository = auditRepository;
        }

        public async Task<Product?> GetById(int id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task<Product> Add(Product product)
        {
            var check = await _productRepository.GetByName(product.Name);
            if (check is not null) throw new Exception("Tento produkt jiz existuje");

            var value = await _productRepository.Add(product);

            await _auditRepository.Add(new AuditLog
            {
                DateTime = DateTime.Now,
                Message = $"Product with id {value.ProductId} and name {value.Name} was added"
            });
            return value;
        }

        public async Task<Product> Update(Product product)
        {
            return await _productRepository.Update(product);
        }

        public async Task Remove(Product product)
        {
            await _productRepository.Remove(product);
        }

        public async Task<ICollection<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }
    }
}
