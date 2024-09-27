
namespace WarehousesEvidence.Interface.Actions
{
    public class ExitAction : IAction
    {
        public string Description => "Ukoncit aplikaci";

        public Task Show()
        {
            Environment.Exit(0);

            return Task.CompletedTask;
        }
    }
}
