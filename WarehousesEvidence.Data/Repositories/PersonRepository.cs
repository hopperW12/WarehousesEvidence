using WarehousesEvidence.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace WarehousesEvidence.Data.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person?> GetById(int id);
    }

    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(DbContext dbContext) : base(dbContext)
        {

        }

        public Task<Person?> GetById(int id)
        {
            return base.Query().FirstOrDefaultAsync(x => x.PersonId == id); 
        }
    }
}
