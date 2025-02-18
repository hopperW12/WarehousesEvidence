using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using WarehousesEvidence.App.Services;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Web.Models;

namespace WarehousesEvidence.Web.Components.ProductsTable.Modals;

public partial class ProductEditModal
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; }
    [Parameter]
    public ProductEditModel FormModel { get; set; }
    
    [Inject]
    public ISnackbar _snackbar { get; set; }
    [Inject] 
    public IProductService _productService { get; set; }

    private ICollection<string> ExistingNames { get; set; }

    private EditContext? EditContext { get; set; }
    private ValidationMessageStore? MessageStore { get; set; }
    
    private void Cancel() => MudDialog.Cancel();
    
    protected override async Task OnParametersSetAsync()
    {
        var products = await _productService.GetAll();

        ExistingNames = products
            .Where(e => e.Id != FormModel.Id)
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
        
        if (ExistingNames.Contains(FormModel.Name))
        {
            MessageStore?.Add(() => FormModel.Name, "Tento název již existuje");
            EditContext?.NotifyValidationStateChanged();
        }
    }

    private async Task OnSubmit()
    {
        var product = new Product
        {
            Id = FormModel.Id,
            Name = FormModel.Name
        };
        
        await _productService.Update(product);
        
        _snackbar.Add("Produkt upraven", Severity.Success);
        MudDialog.Close();
    }

    private async Task Remove()
    {
        var product = new Product
        {
            Id = FormModel.Id,
            Name = FormModel.Name
        };

        await _productService.Remove(product);
        
        _snackbar.Add("Produkt odstraněn", Severity.Success);
        MudDialog.Close();
    }
}