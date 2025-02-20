using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WarehousesEvidence.Data;

namespace WarehousesEvidence.Interface.Extensions;

public static class HostServices
{
    public static void MigrateDatabase(this IHost host) 
    {
        using var scope = host.Services.CreateScope();
        
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<DataDbContext>();
        
        dbContext.Database.EnsureCreated();
    }
}