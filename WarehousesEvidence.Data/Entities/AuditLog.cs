namespace WarehousesEvidence.Data.Entities
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
    }
}
