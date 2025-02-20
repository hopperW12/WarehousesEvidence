
using WarehousesEvidence.Data.Repositories;

namespace WarehousesEvidence.Interface.Actions
{
    public class AuditListAction : IAction
    {
        private IAuditRepository _auditRepository;

        public AuditListAction(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public string Description => "Zobrazit historii skladove evidence";

        public async Task<Result> Show()
        {
            Console.WriteLine("\nHistorie\n");

            var audits = (await _auditRepository.GetAll()).OrderByDescending(e => e.DateTime);
            foreach (var audit in audits)
            {
                Console.WriteLine($"{audit.DateTime.ToString("dd.MM.yyyy HH:mm:ss")} - {audit.Message}");
            }

            return Result.Ok();
        }
    }
}
