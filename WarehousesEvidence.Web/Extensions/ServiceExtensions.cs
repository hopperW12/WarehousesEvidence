using Microsoft.EntityFrameworkCore;
using WarehousesEvidence.Data;

namespace WarehousesEvidence.Web.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContextFactory<DataDbContext>(options =>
        {
            options.UseSqlite("Data Source=WarehousesEvidence.db");
        });
            
        return services;
    }
}