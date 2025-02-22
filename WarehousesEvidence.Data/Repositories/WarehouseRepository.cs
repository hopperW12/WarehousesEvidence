using Microsoft.EntityFrameworkCore;
using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Data.Repositories
{
    public interface IWarehouseRepository : IRepository
    {
        public Task<Warehouse?> Add(Warehouse warehouse);
        public Task<Warehouse?> Update(Warehouse warehouse);
        public Task<bool?> Remove(Warehouse warehouse);
        
        public Task<Warehouse?> GetById(int id);
        public Task<Warehouse?> GetByIdWithIncludes(int id);
        public Task<Warehouse?> GetBySlag(string slag);
        public Task<ICollection<Warehouse>> GetAll();
        public Task<ICollection<Warehouse>> GetAllWithIncludes();
    }

    public class WarehouseRepository : Repository, IWarehouseRepository
    {
        public WarehouseRepository(IDbContextFactory<DataDbContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<Warehouse?> Add(Warehouse warehouse)
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<Warehouse>();

            dbSet.Add(warehouse);
            
            await context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<Warehouse?> Update(Warehouse warehouse)
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<Warehouse>();

            dbSet.Update(warehouse);
            
            await context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<bool?> Remove(Warehouse warehouse)
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<Warehouse>();

            dbSet.Remove(warehouse);
            
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Warehouse?> GetById(int id)
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<Warehouse>();

            return await dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Warehouse?> GetByIdWithIncludes(int id)
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<Warehouse>();

            return await dbSet
                .AsNoTracking()
                .Include(e => e.WarehouseProducts)
                    .ThenInclude(e => e.Product)
                .Include(e => e.Products)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Warehouse?> GetBySlag(string slag)
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<Warehouse>();

            return await dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.SlagName == slag);
        }

        public async Task<ICollection<Warehouse>> GetAll()
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<Warehouse>();

            return await dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<Warehouse>> GetAllWithIncludes()
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<Warehouse>();

            return await dbSet
                .AsNoTracking()
                .Include(e => e.Products)
                .Include(e => e.WarehouseProducts)
                    .ThenInclude(e => e.Product)
                .ToListAsync();
        }
    }
}
