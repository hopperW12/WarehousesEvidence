using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sharprompt;
using Sharprompt.Fluent;
using WarehousesEvidence.Interface.Actions;

namespace WarehousesEvidence.Interface
{
    public class Application : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Application(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var actions = scope.ServiceProvider.GetServices<IAction>().ToList();

            while (cancellationToken.CanBeCanceled)
            {
                Console.WriteLine("\n       Vitej v evidenci skladů     \n");

                var selectAction = Prompt.Select<IAction>(o => o.WithMessage("Vyber akce")
                    .WithItems(actions)
                    .WithTextSelector(a => a.Description));
                var result = await selectAction.Show();
                if (result is ResultExitApp)
                {
                    Console.WriteLine("\nUkoncuji aplikaci.....\n");
                    return;
                }

                Console.WriteLine("\nPro pokracovani prosim zmackni klavesu");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("\nUkoncuji aplikaci.....\n");
            return Task.CompletedTask;
        }
    }
}
