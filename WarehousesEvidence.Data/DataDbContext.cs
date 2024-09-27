using WarehousesEvidence.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace WarehousesEvidence.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataDbContext).Assembly);
        }
    }
}
