using Microsoft.Extensions.DependencyInjection;
using Sharprompt;
using Sharprompt.Fluent;
using System.Reflection;
using WarehousesEvidence.Data.Repositories;
using WarehousesEvidence.Interface.Actions;

namespace WarehousesEvidence.Interface
{
    public class Application
    {
        private IWarehouseRepository _warehouseRepository;
        private IProductRepository _productRepository;

        private List<IAction> _actions;

        public Application(IWarehouseRepository warehouseRepository, IProductRepository productRepository)
        {
            _warehouseRepository = warehouseRepository;
            _productRepository = productRepository;

            _actions = new List<IAction>(); 
        }

        public void CreateActions(IServiceProvider provider)
        {
            provider.GetServices<IAction>().ToList().ForEach(_actions.Add);
        }

        public void Run()
        {
            MainMenu();
        }

        private async void MainMenu()
        {
            Console.WriteLine("\n       Vitej v evidenci skladů     \n");

            var selectAction = Prompt.Select<IAction>(o => o.WithMessage("Vyber akce")
                                                            .WithItems(_actions)
                                                            .WithTextSelector(a => a.Description));
            await selectAction.Show();

            Console.ReadLine();
            Console.Clear();

            MainMenu();
        }
    }
}
