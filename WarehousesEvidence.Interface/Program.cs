using WarehousesEvidence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WarehousesEvidence.Core.Extensions;
using WarehousesEvidence.Interface.Extensions;

namespace WarehousesEvidence.Interface
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = CreateServices();

            //Database migrate
            var DbContext = services.GetRequiredService<DbContext>();
            DbContext.Database.Migrate();

            //Run application
            var app = services.GetRequiredService<Application>();
            app.CreateActions(services); 

            app.Run();
        }

        public static ServiceProvider CreateServices()
        {
            var services = new ServiceCollection();

            //Database
            services.AddDbContext<DbContext, DataDbContext>(options => options.UseSqlite("Data Source=WarehousesEvidence.db"));

            //Repositories
            services.AddRepositories();

            //Services
            services.AddServices();

            //Add menu action
            services.AddMenuAction();

            //Application
            services.AddSingleton<Application>();

            return services.BuildServiceProvider();
        }
    }
}