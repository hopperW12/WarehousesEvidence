using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WarehousesEvidence.App.Services;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Web.Components.ProductsTable.Modals;
using WarehousesEvidence.Web.Mapper;
using WarehousesEvidence.Web.Models;

namespace WarehousesEvidence.Web.Components.ProductsTable;

public partial class ProductsTable
{
    [Inject]
    private IDialogService _dialogService { get; set; }
    [Inject] 
    public IProductService _productService { get; set; }    
    [Inject]
    public IProductModelMapper _mapper { get; set; }
    
    private ICollection<Product> Products { get; set; } = new List<Product>();

    protected override async Task OnInitializedAsync()
    {
        await UpdateTable();
    }

    private async Task UpdateTable()
    {
        Products = await _productService.GetAll();
        StateHasChanged();
    }

    private async Task RowClickEvent(TableRowClickEventArgs<Product> e)
    {
        var product = e.Item;
        if (product == null) return;

        await EditProduct(product);
    }

    private async Task CreateProduct()
    {
        var parameters = new DialogParameters<ProductCreateModal> { { x => x.FormModel, new ProductCreateModel() } };
        var dialog = await _dialogService.ShowAsync<ProductCreateModal>("Create product", parameters);
        var result = await dialog.Result;

        if (result is not { Canceled: true })
            await UpdateTable();
    }
    
    private async Task EditProduct(Product product)
    {
        var model = new ProductEditModel();
        _mapper.Map(product, model);
        
        var parameters = new DialogParameters<ProductEditModal> { { x => x.FormModel, model } };
        var dialog = await _dialogService.ShowAsync<ProductEditModal>("Edit product", parameters);
        var result = await dialog.Result;

        if (result is not { Canceled: true })
            await UpdateTable();
    }
}