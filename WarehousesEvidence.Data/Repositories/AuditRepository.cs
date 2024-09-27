using Microsoft.EntityFrameworkCore;
using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Data.Repositories
{
    public interface IAuditRepository : IRepository<AuditLog>
    {
        Task<AuditLog?> GetById(int id);
    }

    public class AuditRepository : Repository<AuditLog>, IAuditRepository
    {
        public AuditRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<AuditLog?> GetById(int id)
        {
            return await base.Query().FirstOrDefaultAsync(e => e.AuditLogId == id);
        }
    }
}
