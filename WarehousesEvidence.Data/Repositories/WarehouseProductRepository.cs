using Microsoft.EntityFrameworkCore;
using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Data.Repositories;

public interface IWarehouseProductRepository : IRepository
{
    Task Add(WarehouseProduct warehouseProduct);
    Task Update(WarehouseProduct warehouseProduct);
    Task<bool> Remove(WarehouseProduct warehouseProduct);
}

public class WarehouseProductRepository : Repository, IWarehouseProductRepository
{
    public WarehouseProductRepository(IDbContextFactory<DataDbContext> contextFactory) : base(contextFactory)
    {
        
    }

    public async Task Add(WarehouseProduct warehouseProduct)
    {
        await using var context = await ContextFactory.CreateDbContextAsync();
        var dbSet = context.Set<WarehouseProduct>();

        dbSet.Add(warehouseProduct);

        await context.SaveChangesAsync();
    }

    public async Task Update(WarehouseProduct warehouseProduct)
    {
        await using var context = await ContextFactory.CreateDbContextAsync();
        var dbSet = context.Set<WarehouseProduct>();

        dbSet.Update(warehouseProduct);

        await context.SaveChangesAsync();
    }

    public async Task<bool> Remove(WarehouseProduct warehouseProduct)
    {
        await using var context = await ContextFactory.CreateDbContextAsync();
        var dbSet = context.Set<WarehouseProduct>();

        dbSet.Remove(warehouseProduct);

        await context.SaveChangesAsync();
        return true;
    }
}