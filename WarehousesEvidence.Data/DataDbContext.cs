using WarehousesEvidence.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace WarehousesEvidence.Data
{
    public class DataDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataDbContext).Assembly);
        }
    }
}
