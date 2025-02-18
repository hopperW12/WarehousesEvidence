using Microsoft.AspNetCore.Components;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Data.Repositories;

namespace WarehousesEvidence.Web.Components.AuditLogTable;

public partial class AuditLogTable
{
    [Inject] 
    public IAuditRepository _auditRepository { get; set; }
    
    private ICollection<AuditLog> Logs { get; set; } = new List<AuditLog>();

    protected override async Task OnInitializedAsync()
    {
        Logs = await _auditRepository.GetAll();
    }
}