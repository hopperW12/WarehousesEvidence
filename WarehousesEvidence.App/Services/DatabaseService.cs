using WarehousesEvidence.Data.Repositories;

namespace WarehousesEvidence.App.Services;

public interface IDatabaseService : IService
{
    Task<string> ExportToJson();
    Task<bool> ImportFromJson(string json); 
}

public class DatabaseService : IDatabaseService
{
    private readonly IDatabaseRepository _databaseRepository;

    public DatabaseService(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository;
    }

    public async Task<string> ExportToJson()
    {
        return await _databaseRepository.ExportToJson();
    }

    public async Task<bool> ImportFromJson(string json)
    {
        return await _databaseRepository.ImportFromJson(json);
    }
}