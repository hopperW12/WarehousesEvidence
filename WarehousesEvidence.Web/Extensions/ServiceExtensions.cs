using Microsoft.EntityFrameworkCore;
using WarehousesEvidence.Data;
using WarehousesEvidence.Web.Mapper;

namespace WarehousesEvidence.Web.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSource = configuration["Database:DataSource"];
        
        services.AddDbContextFactory<DataDbContext>(options =>
        {
            options.UseSqlite(dataSource);
        });
            
        return services;
    }

    public static IServiceCollection AddModelMappers(this IServiceCollection services)
    {
        services.Scan(scan =>
        {
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IModelMapper))  
                .AddClasses(classes => classes.AssignableTo(typeof(IModelMapper))) 
                .AsImplementedInterfaces() 
                .WithScopedLifetime());  
        });

        return services;
    }
}