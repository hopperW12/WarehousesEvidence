using WarehousesEvidence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WarehousesEvidence.Interface
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = CreateServices();

            //Database migrate
            var DbContext = services.GetRequiredService<DataDbContext>();
            DbContext.Database.Migrate();

            //Run application
            var app = services.GetRequiredService<Application>();
            app.Run();
        }

        public static ServiceProvider CreateServices()
        {
            var services = new ServiceCollection();

            //Database
            services.AddDbContext<DataDbContext>(options => options.UseSqlite("Data Source=WarehousesEvidence.db"));

            //Application
            services.AddSingleton<Application>();

            return services.BuildServiceProvider();
        }
    }
}