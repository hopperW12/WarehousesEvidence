using Microsoft.AspNetCore.Components;
using WarehousesEvidence.App.Services;
using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Web.Components.Pages;

public partial class WarehousePage
{
    [Inject]
    public IWarehouseService _warehouseService { get; set; }
    [Inject]
    public NavigationManager _navigation { get; set; }
    
    [Parameter]
    public string? WarehouseSlug { get; set; }
    
    private Warehouse? Warehouse { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (WarehouseSlug == null)
        {
            _navigation.NavigateTo("warehouses");
            return;
        }
        
        var warehouse = await _warehouseService.GetBySlug(WarehouseSlug);
        if (warehouse == null)
        {
            _navigation.NavigateTo("warehouses");
            return;
        }

        Warehouse = warehouse;
    }
}