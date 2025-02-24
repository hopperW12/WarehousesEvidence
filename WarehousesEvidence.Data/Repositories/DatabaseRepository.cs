using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;

namespace WarehousesEvidence.Data.Repositories;

public interface IDatabaseRepository
{
    Task<string> ExportToJson();
    Task<bool> ImportFromJson(string json); 
}

public class DatabaseRepository : Repository, IDatabaseRepository
{
    public DatabaseRepository(IDbContextFactory<DataDbContext> contextFactory) : base(contextFactory)
    {
        
    }
    
    public async Task<string> ExportToJson()
    {
        var data = new Dictionary<string, object>();
        await using var context = await ContextFactory.CreateDbContextAsync();

        var entityTypes = context.Model.GetEntityTypes();
        foreach (var entityType in entityTypes)
        {
            var dbSet = context.GetType()
                .GetMethod("Set", 1, Type.EmptyTypes)
                ?.MakeGenericMethod(entityType.ClrType)
                .Invoke(context, null) as IQueryable<dynamic>;

            if (dbSet == null) continue;

            var tableData = await dbSet.ToListAsync();
            var tableName = entityType.GetTableName();
            if (tableName != null)
                data[tableName] = tableData;
        }

        return JsonConvert.SerializeObject(data, Formatting.Indented);
    }

    public async Task<bool> ImportFromJson(string json)
    {
        await using var context = await ContextFactory.CreateDbContextAsync();
        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var entityTypes = context.Model.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                var dbSet = context.GetType()
                    .GetMethod("Set", 1, Type.EmptyTypes)
                    ?.MakeGenericMethod(entityType.ClrType)
                    .Invoke(context, null) as IQueryable<object>;
                
                if (dbSet == null) continue;

                await dbSet.ExecuteDeleteAsync();
                await context.SaveChangesAsync();
            }
            
            var data = JsonConvert.DeserializeObject<Dictionary<string, JArray>>(json);
            if (data == null)
                return false;
            
            foreach (var entityType in context.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName == null || !data.TryGetValue(tableName, out var tableData)) continue;
                
                var entityClrType = entityType.ClrType;
                var dbSet = context.GetType()
                    .GetMethod("Set", 1, Type.EmptyTypes)
                    ?.MakeGenericMethod(entityClrType)
                    .Invoke(context, null);

                if (dbSet == null) continue;
                
                var deserializedObjects = tableData.ToObject(typeof(List<>).MakeGenericType(entityClrType));
                var addRangeMethod = typeof(DbSet<>).MakeGenericType(entityClrType)
                    .GetMethod("AddRangeAsync", [typeof(IEnumerable<>).MakeGenericType(entityClrType), typeof(CancellationToken)
                    ]);
                
                if (addRangeMethod != null)
                    await (Task)addRangeMethod.Invoke(dbSet, [deserializedObjects, CancellationToken.None])!;
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            await transaction.RollbackAsync();
            return false;
        }
    }
}