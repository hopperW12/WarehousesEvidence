using Slugify;
using WarehousesEvidence.Data.Entities;
using WarehousesEvidence.Data.Repositories;

namespace WarehousesEvidence.App.Services
{
    public interface IWarehouseService : IService
    {
        public Task<Warehouse?> AddWarehouse(Warehouse warehouse);
        public Task DeleteWarehouse(Warehouse warehouse);
        public Task<Warehouse?> UpdateWarehouse(Warehouse warehouse);

        public Task<Warehouse?> GetBySlug(string slug);
        public Task<Warehouse?> GetByIdWithIncludes(int id);
        public Task<ICollection<Warehouse>> GetAll();
        public Task<ICollection<Warehouse>> GetAllWithIncludes();
    }

    public class WarehouseService : IWarehouseService
    {
        private IWarehouseRepository _warehouseRepository;
        private IAuditRepository _auditRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository, IAuditRepository auditRepository)
        {
            _warehouseRepository = warehouseRepository;
            _auditRepository = auditRepository;
        }

        public async Task<Warehouse?> AddWarehouse(Warehouse warehouse)
        {
            var slag = new SlugHelper().GenerateSlug(warehouse.Name);
            warehouse.SlagName = slag;

            var warehouses = await _warehouseRepository.GetAll();
            var slags = warehouses.Select(e => e.SlagName);

            if (slags.Contains(warehouse.SlagName))
                return null;
            
            await _auditRepository.Add(new AuditLog
            {
                DateTime = DateTime.Now,
                Message = $"Warehouse with id {warehouse.Id} and name {warehouse.Name} was added"
            });
            return await _warehouseRepository.Add(warehouse);
        }

        public async Task DeleteWarehouse(Warehouse warehouse)
        {
            var slag = new SlugHelper().GenerateSlug(warehouse.Name);
            warehouse.SlagName = slag;

            var warehouses = await _warehouseRepository.GetAll();
            var slags = warehouses
                .Where(e => e.Id != warehouse.Id)
                .Select(e => e.SlagName);

            if (slags.Contains(warehouse.SlagName))
                return;
            
            await _auditRepository.Add(new AuditLog
            {
                DateTime = DateTime.Now,
                Message = $"Warehouse with id {warehouse.Id} and name {warehouse.Name} has been removed"
            });
            await _warehouseRepository.Remove(warehouse);
        }

        public async Task<Warehouse?> GetBySlug(string slug)
        {
            return await _warehouseRepository.GetBySlag(slug);
        }

        public async Task<Warehouse?> GetByIdWithIncludes(int id)
        {
            return await _warehouseRepository.GetByIdWithIncludes(id);
        }

        public async Task<ICollection<Warehouse>> GetAll()
        {
            return await _warehouseRepository.GetAll();
        }

        public async Task<ICollection<Warehouse>> GetAllWithIncludes()
        {
            return await _warehouseRepository.GetAllWithIncludes();
        }

        public async Task<Warehouse?> UpdateWarehouse(Warehouse warehouse)
        {
            var slag = new SlugHelper().GenerateSlug(warehouse.Name);
            warehouse.SlagName = slag;

            var warehouses = await _warehouseRepository.GetAll();
            var slags = warehouses
                .Where(e => e.Id != warehouse.Id)
                .Select(e => e.SlagName);

            if (slags.Contains(warehouse.SlagName))
                return null;
            
            var oldWarehouse = await _warehouseRepository.GetById(warehouse.Id);
            if (oldWarehouse is null) throw new Exception("Warehouse nebyl nalezen");

            var returnValue = await _warehouseRepository.Update(warehouse);

            if (oldWarehouse.Name != warehouse.Name)
            {
                await _auditRepository.Add(new AuditLog
                {
                    DateTime = DateTime.Now,
                    Message = $"Rename Warehouse from {oldWarehouse.Name} to {warehouse.Name}"
                });
            }

            if (oldWarehouse.Address != warehouse.Address)
            {
                await _auditRepository.Add(new AuditLog
                {
                    DateTime = DateTime.Now,
                    Message = $"Change Warehouse Address from {oldWarehouse.Address} to {warehouse.Address}"
                });
            }

            if (oldWarehouse.WarehouseProducts != warehouse.WarehouseProducts)
            {
                await _auditRepository.Add(new AuditLog
                {
                    DateTime = DateTime.Now,
                    Message = $"Change Warehouse Products"
                });
            }


            return returnValue;
        }
    }
}
