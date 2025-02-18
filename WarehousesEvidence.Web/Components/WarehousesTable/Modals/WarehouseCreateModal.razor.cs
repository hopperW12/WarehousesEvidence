using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Slugify;
using WarehousesEvidence.App.Services;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Web.Models;

namespace WarehousesEvidence.Web.Components.WarehousesTable.Modals;

public partial class WarehouseCreateModal
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; }
    [Parameter]
    public WarehouseCreateModel FormModel { get; set; }
    
    [Inject]
    public ISnackbar _snackbar { get; set; }
    [Inject] 
    public IWarehouseService _warehouseService { get; set; }

    private ICollection<string> ExistingSlugs { get; set; }
    private ICollection<string> ExistingNames { get; set; }

    private EditContext? EditContext { get; set; }
    private ValidationMessageStore? MessageStore { get; set; }
    
    private void Cancel() => MudDialog.Cancel();

    protected override async Task OnParametersSetAsync()
    {
        var warehouses = await _warehouseService.GetAll();

        ExistingNames = warehouses.Select(e => e.Name).ToList();
        ExistingSlugs = warehouses.Select(e => e.SlagName).ToList();
        
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

        var slug = new SlugHelper().GenerateSlug(FormModel.Name);
        if (ExistingSlugs.Contains(slug))
        {
            MessageStore?.Add(() => FormModel.Name, "Tento název již existuje");
            EditContext?.NotifyValidationStateChanged();
        }
    }

    private async Task OnSubmit()
    {
        var warehouse = new Warehouse
        {
            Name = FormModel.Name,
            Address = FormModel.Address
        };

        var result = await _warehouseService.AddWarehouse(warehouse);
        if (result == null)  
        {
            _snackbar.Add("Nastala chyba", Severity.Error);
            return;
        }
        
        _snackbar.Add("Sklad vytvořen", Severity.Success);
        MudDialog.Close();
    }
}