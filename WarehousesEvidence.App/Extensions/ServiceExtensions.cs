using Microsoft.Extensions.DependencyInjection;
using WarehousesEvidence.App.Services;

namespace WarehousesEvidence.App.Extensions
{
    public static class ServiceExtensions
    {
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
