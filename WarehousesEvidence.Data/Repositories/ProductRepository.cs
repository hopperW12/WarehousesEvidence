using Microsoft.EntityFrameworkCore;
using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Data.Repositories
{
    public interface IProductRepository : IRepository
    {
        Task<Product?> Add(Product product);
        Task<Product?> Update(Product product);
        Task<Product?> Remove(Product product);
        
        Task<Product?> GetById(int id);
        Task<Product?> GetByName(string name);

        Task<ICollection<Product>> GetAll();
    }

    public class ProductRepository : Repository, IProductRepository
    {
        public ProductRepository(IDbContextFactory<DataDbContext> contextFactory) : base(contextFactory)
        {
        }


        public async Task<Product?> Add(Product product)
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<Product>();

            dbSet.Add(product);
            
            await context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> Update(Product product)
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<Product>();

            dbSet.Update(product);

            await context.SaveChangesAsync();
            return product;
        }
        
        public async Task<Product?> Remove(Product product)
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<Product>();

            dbSet.Remove(product);

            await context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> GetById(int id)
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<Product>();

            return await dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Product?> GetByName(string name)
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<Product>();

            return await dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Name == name);
        }

        public async Task<ICollection<Product>> GetAll()
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<Product>();

            return await dbSet
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
