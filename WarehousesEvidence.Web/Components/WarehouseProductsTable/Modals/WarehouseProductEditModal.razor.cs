using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using WarehousesEvidence.App.Services;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Web.Models;

namespace WarehousesEvidence.Web.Components.WarehouseProductsTable.Modals;

public partial class WarehouseProductEditModal
{
    [Inject]
    public ISnackbar _snackbar { get; set; }
    [Inject]
    public IWarehouseProductService _warehouseProductService { get; set; }
    
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; }
    [Parameter]
    public WarehouseProductEditModel FormModel { get; set; }
    
    private EditContext? EditContext { get; set; }
    private ValidationMessageStore? MessageStore { get; set; }
    
    private void Cancel() => MudDialog.Cancel();

    protected override void OnParametersSet()
    {
        EditContext = new EditContext(FormModel);
        MessageStore = new ValidationMessageStore(EditContext);
        EditContext.OnValidationRequested += OnValidationRequested;
    }

    private void OnValidationRequested(object? sender, ValidationRequestedEventArgs args)
    {
        MessageStore?.Clear();

        if (FormModel.Quantity <= 0)
        {
            MessageStore?.Add(() => FormModel.Quantity, "Zadej větší číslo než 0");
            EditContext?.NotifyValidationStateChanged();
        }
    }

    private async Task OnSubmit()
    {
        var item = new WarehouseProduct
        {
            ProductId = FormModel.Product.Id,
            WarehouseId = FormModel.Warehouse.Id,
            Quantity = FormModel.Quantity
        };

        await _warehouseProductService.Update(item);

        _snackbar.Add("Produkt byl upraven", Severity.Success);
        MudDialog.Close();
    }

    private async Task Remove(MouseEventArgs arg)
    {
        var item = new WarehouseProduct
        {
            ProductId = FormModel.Product.Id,
            WarehouseId = FormModel.Warehouse.Id,
            Quantity = FormModel.Quantity
        };
        
        await _warehouseProductService.Remove(item);

        _snackbar.Add("Produkt byl odstranen", Severity.Success);
        MudDialog.Close();
    }
}