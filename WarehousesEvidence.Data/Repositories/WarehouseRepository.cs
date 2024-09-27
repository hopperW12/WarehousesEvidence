using Microsoft.EntityFrameworkCore;
using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Data.Repositories
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        public Task<Warehouse?> GetById(int id);
        public Task<ICollection<Warehouse>> GetAllWithIncludes();
    }

    public class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(DbContext dbContext) : base(dbContext)
        {

        }

        public async Task<ICollection<Warehouse>> GetAllWithIncludes()
        {
            return await base.Query()
                .Include(e => e.Products)
                .ToListAsync();
        }

        public Task<Warehouse?> GetById(int id)
        {
            return base.Query().FirstOrDefaultAsync(x => x.WarehouseId == id);
        }
    }
}
