using Microsoft.Extensions.DependencyInjection;
using WarehousesEvidence.Data.Repositories;
using WarehousesEvidence.Data.Services;

namespace WarehousesEvidence.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                services.Scan(scan => scan
                    .FromAssembliesOf(typeof(IRepository<>))  
                    .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>))) 
                    .AsImplementedInterfaces() 
                    .WithScopedLifetime());  
            });

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                services.Scan(scan => scan
                    .FromAssembliesOf(typeof(IService))
                    .AddClasses(classes => classes.AssignableTo(typeof(IService)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
            });

            return services;
        }
    }
}
