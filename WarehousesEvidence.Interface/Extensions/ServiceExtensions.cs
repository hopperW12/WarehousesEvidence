using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarehousesEvidence.Data;
using WarehousesEvidence.Interface.Actions;

namespace WarehousesEvidence.Interface.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMenuAction(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                services.Scan(scan => scan
                    .FromAssembliesOf(typeof(IAction))
                    .AddClasses(classes => classes.AssignableTo<IAction>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
            });

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var dataSource = configuration["Database:DataSource"];
            
            services.AddDbContextFactory<DataDbContext>(options =>
            {
                options.UseSqlite(dataSource);
            });
            
            return services;
        }
    }
}
