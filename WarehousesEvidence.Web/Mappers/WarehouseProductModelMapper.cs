using WarehousesEvidence.App.Services;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Web.Models;

namespace WarehousesEvidence.Web.Mapper;

public interface IWarehouseProductModelMapper : IModelMapper
{
    Task Map(WarehouseProductCreateModel model, Warehouse warehouse);
    void Map(WarehouseProductCreateModel model, WarehouseProduct warehouseProduct);
    
    void Map(WarehouseProduct warehouseProduct, WarehouseProductEditModel model);
    void Map(WarehouseProductEditModel model, WarehouseProduct warehouseProduct);
}

public class WarehouseProductModelMapper : IWarehouseProductModelMapper
{
    private IProductService _productService { get; set; }

    public WarehouseProductModelMapper(IProductService productService)
    {
        _productService = productService;
    }

    public async Task Map(WarehouseProductCreateModel model, Warehouse warehouse)
    {
        var products = await _productService.GetAll();
        var unavailableProducts = warehouse.WarehouseProducts
            .Select(e => e.Product);
        var availableProducts = products
            .Where(e => unavailableProducts.All(f => f.Id != e.Id))
            .ToList();

        model.Warehouse = warehouse;
        model.AvailableProducts = availableProducts;
        if (availableProducts.Count > 0)
            model.Product = availableProducts.First();
        model.Quantity = 1;
    }

    public void Map(WarehouseProductCreateModel model, WarehouseProduct warehouseProduct)
    {
        warehouseProduct.WarehouseId = model.Warehouse.Id;
        warehouseProduct.ProductId = model.Product.Id;
        warehouseProduct.Quantity = model.Quantity;
    }

    public void Map(WarehouseProduct warehouseProduct, WarehouseProductEditModel model)
    {
        model.Warehouse = warehouseProduct.Warehouse;
        model.Product = warehouseProduct.Product;
        model.Quantity = warehouseProduct.Quantity;
    }

    public void Map(WarehouseProductEditModel model, WarehouseProduct warehouseProduct)
    {
        warehouseProduct.WarehouseId = model.Warehouse.Id;
        warehouseProduct.ProductId = model.Product.Id;
    }
}