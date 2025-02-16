using Microsoft.EntityFrameworkCore;

namespace WarehousesEvidence.Data.Repositories
{
    public interface IRepository 
    {
        
    }

    public abstract class Repository : IRepository
    {
        protected readonly IDbContextFactory<DataDbContext> ContextFactory;
        
        public Repository(IDbContextFactory<DataDbContext> contextFactory)
        {
            ContextFactory = contextFactory;
        }
    }
    
}
