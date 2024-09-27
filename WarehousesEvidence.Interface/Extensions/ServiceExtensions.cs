using Microsoft.Extensions.DependencyInjection;
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
                    .WithSingletonLifetime());
            });

            return services;
        }
    }
}
