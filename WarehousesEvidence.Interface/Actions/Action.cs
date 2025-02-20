namespace WarehousesEvidence.Interface.Actions
{
    public interface IAction
    {
        Task<Result> Show();

        string Description { get; }
    }

    public abstract class Result
    {
        public static Result Ok()
        {
            return new ResultOk();
        }

        public static Result ExitApplication()
        {
            return new ResultExitApp();
        }
    }

    public class ResultOk : Result
    {
        
    }

    public class ResultExitApp : Result
    {
        
    }
}
