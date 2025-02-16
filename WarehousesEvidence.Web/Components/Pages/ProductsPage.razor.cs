using Microsoft.AspNetCore.Components;
using WarehousesEvidence.App.Services;
using WarehousesEvidence.Data.Entities;

namespace WarehousesEvidence.Web.Components.Pages;

public partial class ProductsPage
{
    [Inject] 
    public IProductService _productService { get; set; }
    
    private IEnumerable<Product> Products { get; set; } = new List<Product>();
    
    protected override async Task OnInitializedAsync()
    {
        Products = await _productService.GetAll();
    }
}