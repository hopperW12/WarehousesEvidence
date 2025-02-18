using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Web.Models;

namespace WarehousesEvidence.Web.Mapper;

public interface IWarehouseModelMapper : IModelMapper
{
    void Map(WarehouseCreateModel model, Warehouse warehouse);
    void Map(Warehouse warehouse, WarehouseEditModel model);
    void Map(WarehouseEditModel model, Warehouse warehouse);
}

public class WarehouseModelMapper : IWarehouseModelMapper
{
    public void Map(WarehouseCreateModel model, Warehouse warehouse)
    {
        warehouse.Name = model.Name;
        warehouse.Address = model.Address;
    }

    public void Map(Warehouse warehouse, WarehouseEditModel model)
    {
        model.Id = warehouse.Id;
        model.Name = warehouse.Name;
        model.Address = warehouse.Address;
    }

    public void Map(WarehouseEditModel model, Warehouse warehouse)
    {
        warehouse.Id = model.Id;
        warehouse.Name = model.Name;
        warehouse.Address = model.Address;
    }
}