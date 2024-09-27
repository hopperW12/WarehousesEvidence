using WarehousesEvidence.Data;
using WarehousesEvidence.Data.Repositories;

namespace WarehousesEvidence.Interface
{
    public class Application
    {
        private PersonRepository PersonRepository { get; set; }

        public Application(DataDbContext dataDbContext)
        {
            PersonRepository = new PersonRepository(dataDbContext);
        }

        public void Run()
        {

        }
    }
}
