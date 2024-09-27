using Microsoft.EntityFrameworkCore;
using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetById(int id);
        Task<Product?> GetByName(string name);
    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }

        public async Task<Product?> GetById(int id)
        {
            return await base.Query().FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product?> GetByName(string name)
        {
            return await base.Query().FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
