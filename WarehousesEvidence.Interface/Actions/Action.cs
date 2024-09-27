namespace WarehousesEvidence.Interface.Actions
{
    public interface IAction
    {
        Task Show();

        string Description { get; }
    }
}
