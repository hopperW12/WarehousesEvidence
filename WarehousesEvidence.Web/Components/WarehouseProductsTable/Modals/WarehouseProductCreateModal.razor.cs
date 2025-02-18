using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using WarehousesEvidence.App.Services;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Web.Mapper;
using WarehousesEvidence.Web.Models;

namespace WarehousesEvidence.Web.Components.WarehouseProductsTable.Modals;

public partial class WarehouseProductCreateModal
{
    [Inject]
    public ISnackbar _snackbar { get; set; }
    [Inject]
    public IWarehouseProductService _warehouseProductService { get; set; }
    [Inject]
    public IWarehouseProductModelMapper _mapper { get; set; }
    
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; }
    [Parameter]
    public WarehouseProductCreateModel FormModel { get; set; }
    
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
        var item = new WarehouseProduct();
        _mapper.Map(FormModel, item);

        await _warehouseProductService.Add(item);

        _snackbar.Add("Produkt byl přidán", Severity.Success);
        MudDialog.Close();
    }
}