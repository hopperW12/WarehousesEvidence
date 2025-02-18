using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WarehousesEvidence.App.Services;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Web.Components.WarehousesTable.Modals;
using WarehousesEvidence.Web.Mapper;
using WarehousesEvidence.Web.Models;

namespace WarehousesEvidence.Web.Components.WarehousesTable;

public partial class WarehousesTable
{
    [Inject]
    public ISnackbar _snackbar { get; set; }
    [Inject]
    private IDialogService _dialogService { get; set; }
    [Inject] 
    public IWarehouseService _warehouseService { get; set; }
    [Inject]
    public IWarehouseModelMapper _mapper { get; set; }
    [Inject]
    public NavigationManager _navigationManager { get; set; }
    
    private IEnumerable<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
    
    protected override async Task OnInitializedAsync()
    {
        await UpdateTable();
    }

    private async Task UpdateTable()
    {
        Warehouses = await _warehouseService.GetAllWithIncludes();
        StateHasChanged();
    }

    private async Task CreateWarehouse()
    {
        var parameters = new DialogParameters<WarehouseCreateModal> { { x => x.FormModel, new WarehouseCreateModel() } };
        var dialog = await _dialogService.ShowAsync<WarehouseCreateModal>("Create warehouse", parameters);
        var result = await dialog.Result;

        if (result is not { Canceled: true })
        {
            await UpdateTable();
            _navigationManager.NavigateTo(_navigationManager.Uri, true);
        }
    }

    private async Task EditWarehouse(Warehouse warehouse)
    {
        var model = new WarehouseEditModel();
        _mapper.Map(warehouse, model);
        
        var parameters = new DialogParameters<WarehouseEditModal> { { x => x.FormModel, model } };
        var dialog = await _dialogService.ShowAsync<WarehouseEditModal>("Edit warehouse", parameters);
        var result = await dialog.Result;

        if (result is not { Canceled: true })
        {
            await UpdateTable();
            _navigationManager.NavigateTo(_navigationManager.Uri, true);
        }
    }

    private async Task RowClickEvent(TableRowClickEventArgs<Warehouse> e)
    {
        var warehouse = e.Item;
        if (warehouse == null) return;

        await EditWarehouse(warehouse);
    }
}