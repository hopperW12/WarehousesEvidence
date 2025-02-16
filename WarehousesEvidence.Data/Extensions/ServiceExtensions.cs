using Microsoft.Extensions.DependencyInjection;
using WarehousesEvidence.Data.Repositories;

namespace WarehousesEvidence.Data.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                services.Scan(scan => scan
                    .FromAssembliesOf(typeof(IRepository))  
                    .AddClasses(classes => classes.AssignableTo(typeof(IRepository))) 
                    .AsImplementedInterfaces() 
                    .WithScopedLifetime());  
            });

            return services;
        }
    }
}
