
namespace WarehousesEvidence.Interface.Actions
{
    public class ExitAction : IAction
    {
        public string Description => "Ukoncit aplikaci";

        public Task<Result> Show()
        {
            return Task.FromResult(Result.ExitApplication());
        }
    }
}
