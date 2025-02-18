using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Data.Repositories;

namespace WarehousesEvidence.App.Services;

public interface IWarehouseProductService : IService
{
    Task Add(WarehouseProduct warehouseProduct);
    Task Update(WarehouseProduct warehouseProduct);
    Task<bool> Remove(WarehouseProduct warehouseProduct);
}

public class WarehouseProductService : IWarehouseProductService
{
    private readonly IWarehouseProductRepository _warehouseProductRepository;

    public WarehouseProductService(IWarehouseProductRepository warehouseProductRepository)
    {
        _warehouseProductRepository = warehouseProductRepository;
    }

    public async Task Add(WarehouseProduct warehouseProduct)
    {
        await _warehouseProductRepository.Add(warehouseProduct);
    }

    public async Task Update(WarehouseProduct warehouseProduct)
    {
        await _warehouseProductRepository.Update(warehouseProduct);
    }

    public async Task<bool> Remove(WarehouseProduct warehouseProduct)
    {
        return await _warehouseProductRepository.Remove(warehouseProduct);
    }
}