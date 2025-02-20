using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WarehousesEvidence.App.Extensions;
using WarehousesEvidence.Data.Extensions;
using WarehousesEvidence.Interface.Extensions;

namespace WarehousesEvidence.Interface
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder();
            
            builder.Services.AddDatabase(builder.Configuration);
            builder.Services.AddRepositories();
            builder.Services.AddServices();
            builder.Services.AddMenuAction();
            
            builder.Services.AddHostedService<Application>();
 
            var host = builder.Build();

            host.MigrateDatabase();
            
            await host.StartAsync();
        }
    }
}