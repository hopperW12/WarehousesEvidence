using Microsoft.EntityFrameworkCore;
using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Data.Repositories
{
    public interface IAuditRepository : IRepository
    {
        Task<AuditLog?> Add(AuditLog auditLog);

        Task<ICollection<AuditLog>> GetAll();
        Task<AuditLog?> GetById(int id);
    }

    public class AuditRepository : Repository, IAuditRepository
    {
        public AuditRepository(IDbContextFactory<DataDbContext> contextFactory) : base(contextFactory)
        {
        }
        
        public async Task<AuditLog?> Add(AuditLog auditLog)
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<AuditLog>();

            dbSet.Add(auditLog);

            await context.SaveChangesAsync();
            return auditLog;
        }

        public async Task<ICollection<AuditLog>> GetAll()
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<AuditLog>();
            return await dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AuditLog?> GetById(int id)
        {
            await using var context = await ContextFactory.CreateDbContextAsync();
            var dbSet = context.Set<AuditLog>();
            return await dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.AuditLogId == id);
                
        }
    }
}
