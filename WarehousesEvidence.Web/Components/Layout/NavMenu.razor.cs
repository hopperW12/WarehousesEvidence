using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;
using WarehousesEvidence.App.Services;
using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Web.Components.Layout;

public partial class NavMenu
{
    [Inject]
    public IWarehouseService _warehouseService { get; set; }
    
    private ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();

    protected override async Task OnInitializedAsync()
    {
        Warehouses = await _warehouseService.GetAll();
    }
}