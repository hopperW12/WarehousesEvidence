using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WarehousesEvidence.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Remove(TEntity entity);

        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> Query();
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext DbContext;
        private DbSet<TEntity> DbSet;

        public Repository(DbContext dbContext)
        {
            this.DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            DbContext.Add(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet
                .Where(predicate)
                .ToListAsync();
        }

        public IQueryable<TEntity> Query()
        {
            return DbSet.AsQueryable();
        }

        public async Task Remove(TEntity entity)
        {
            DbContext.Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            DbContext.Update(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }
    }
}
