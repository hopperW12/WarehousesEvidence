using Microsoft.AspNetCore.Components;
using MudBlazor;
using WarehousesEvidence.App.Services;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Web.Components.WarehouseProductsTable.Modals;
using WarehousesEvidence.Web.Components.WarehousesTable.Modals;
using WarehousesEvidence.Web.Mapper;
using WarehousesEvidence.Web.Models;

namespace WarehousesEvidence.Web.Components.WarehouseProductsTable;

public partial class WarehouseProductsTable
{
    [Inject] 
    public IWarehouseService _warehouseService { get; set; }
    [Inject] 
    public IProductService _productService { get; set; }
    [Inject]
    private IDialogService _dialogService { get; set; }
    [Inject]
    public IWarehouseProductModelMapper _mapper { get; set; }
    
    [Parameter]
    public int WarehouseId { get; set; }
    
    private ICollection<WarehouseProduct> Items { get; set; } = new List<WarehouseProduct>();

    protected override async Task OnParametersSetAsync()
    {
        await UpdateTable();
    }

    private async Task UpdateTable()
    {
        var warehouse = await _warehouseService.GetByIdWithIncludes(WarehouseId);
        if (warehouse == null) return;

        Items = warehouse.WarehouseProducts;
        
        StateHasChanged();
    }

    private async Task RowClickEvent(TableRowClickEventArgs<WarehouseProduct> e)
    {
        var warehouseProduct = e.Item;
        if (warehouseProduct == null) return;

        var model = new WarehouseProductEditModel()
        {
            Warehouse = warehouseProduct.Warehouse,
            Product = warehouseProduct.Product,
            Quantity = warehouseProduct.Quantity
        };
        var parameters = new DialogParameters<WarehouseProductEditModal> { { x => x.FormModel, model } };
        var dialog = await _dialogService.ShowAsync<WarehouseProductEditModal>("Edit WarehouseProduct", parameters);
        var result = await dialog.Result;
        
        if (result is not { Canceled: true })
            await UpdateTable();
    }

    private async Task CreateWarehouseProduct()
    {
        var warehouse = await _warehouseService.GetByIdWithIncludes(WarehouseId);
        if (warehouse == null) return;
        
        var model = new WarehouseProductCreateModel();
        await _mapper.Map(model, warehouse);
        
        if (model.AvailableProducts.Count == 0)
            return;
                    
        var parameters = new DialogParameters<WarehouseProductCreateModal> { { x => x.FormModel, model } };
        var dialog = await _dialogService.ShowAsync<WarehouseProductCreateModal>("Create WarehouseProduct", parameters);
        var result = await dialog.Result;

        if (result is not { Canceled: true })
            await UpdateTable();
    }
}