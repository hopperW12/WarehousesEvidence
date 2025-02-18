using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Web.Models;

namespace WarehousesEvidence.Web.Mapper;

public interface IProductModelMapper : IModelMapper
{
    void Map(ProductCreateModel model, Product product);
    void Map(Product product, ProductEditModel model);
    void Map(ProductEditModel model, Product product);
}

public class ProductModelMapper : IProductModelMapper
{
    public void Map(ProductCreateModel model, Product product)
    {
        product.Name = model.Name;
    }

    public void Map(Product product, ProductEditModel model)
    {
        model.Id = product.Id;
        model.Name = product.Name;
    }

    public void Map(ProductEditModel model, Product product)
    {
        product.Id = model.Id;
        product.Name = model.Name;
    }
}