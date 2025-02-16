using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using WarehousesEvidence.App.Services;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Web.Models.Warehouse;

namespace WarehousesEvidence.Web.Components.WarehousesTable.Modals;

public partial class WarehouseEditModal
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; }
    [Parameter]
    public WarehouseEditModel FormModel { get; set; }
    
    [Inject]
    public ISnackbar _snackbar { get; set; }
    [Inject] 
    public IWarehouseService _warehouseService { get; set; }

    private ICollection<string> ExistingNames { get; set; }

    private EditContext? EditContext { get; set; }
    private ValidationMessageStore? MessageStore { get; set; }
    
    private void Cancel() => MudDialog.Cancel();
    
    protected override async Task OnParametersSetAsync()
    {
        var warehouses = await _warehouseService.GetAll();

        ExistingNames = warehouses
            .Where(e => e.WarehouseId != FormModel.Id)
            .Select(e => e.Name).ToList();
        
        EditContext = new EditContext(FormModel);
        MessageStore = new ValidationMessageStore(EditContext);
        EditContext.OnValidationRequested += OnValidationRequested;
    }

    private void OnValidationRequested(object? sender, ValidationRequestedEventArgs args)
    {
        MessageStore?.Clear();

        if (string.IsNullOrWhiteSpace(FormModel.Name))
        {
            MessageStore?.Add(() => FormModel.Name, "Zadej název");
            EditContext?.NotifyValidationStateChanged();
        }
        if (string.IsNullOrWhiteSpace(FormModel.Address))
        {
            MessageStore?.Add(() => FormModel.Address, "Zadej adresu");
            EditContext?.NotifyValidationStateChanged();
        }
        
        if (ExistingNames.Contains(FormModel.Name))
        {
            MessageStore?.Add(() => FormModel.Name, "Tento název již existuje");
            EditContext?.NotifyValidationStateChanged();
        }
    }

    private async Task OnSubmit()
    {
        var warehouse = new Warehouse
        {
            WarehouseId = FormModel.Id,
            Name = FormModel.Name,
            Address = FormModel.Address
        };

        var result = await _warehouseService.UpdateWarehouse(warehouse);
        if (result == null)  
        {
            _snackbar.Add("Nastala chyba", Severity.Error);
            return;
        }
        
        _snackbar.Add("Sklad upraven", Severity.Success);
        MudDialog.Close();
    }
}