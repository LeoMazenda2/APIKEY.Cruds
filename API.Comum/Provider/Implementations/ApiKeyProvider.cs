using API.Comum.Models;
using API.Comum.Provider.Interfaces;
using Azure.Data.Tables;

namespace API.Comum.Provider.Implementations;

public sealed class ApiKeyProvider : IApiKeyProvider
{
    private const string TableName = "XDSobaApiKeys";
    private readonly TableClient _table;

    public ApiKeyProvider(TableServiceClient serviceClient)
    {
        _table = serviceClient.GetTableClient(TableName);
        _table.CreateIfNotExists();
    }

    public async Task<IEnumerable<ApiKeyEntity>> GetAllAsync()
    {
        var apiKeys = new List<ApiKeyEntity>();
        await foreach (var entity in _table.QueryAsync<ApiKeyEntity>())
        {
            apiKeys.Add(entity);
        }
        return apiKeys;
    }

    public async Task<ApiKeyEntity?> GetAsync(string licenseId, string keyId)
    {
        var response = await _table.GetEntityIfExistsAsync<ApiKeyEntity>(licenseId, keyId);
        return response.HasValue ? response.Value : null;
    }
    public async Task<ApiKeyEntity?> FindByHashAsync(string licenseId, string sha256Hash)
    {
        var filter = TableClient.CreateQueryFilter($"PartitionKey eq {licenseId} and Sha256Hash eq {sha256Hash} and IsActive eq true");
        await foreach (var entity in _table.QueryAsync<ApiKeyEntity>(filter))
        {
            return entity;
        }
        return null;
    }

    public async Task AddAsync(ApiKeyEntity entity) => await _table.AddEntityAsync(entity);

    public async Task UpdateAsync(ApiKeyEntity entity) => await _table.UpdateEntityAsync(entity, entity.ETag, TableUpdateMode.Replace);
}