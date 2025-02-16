using WarehousesEvidence.Data;

namespace WarehousesEvidence.Web.Extensions;

public static class WebApplicationExtensions
{
    public static void AddDatabase(this WebApplication app) 
    {
        using var scope = app.Services.CreateScope();
        
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<DataDbContext>();
        
        dbContext.Database.EnsureCreated();
    }
}