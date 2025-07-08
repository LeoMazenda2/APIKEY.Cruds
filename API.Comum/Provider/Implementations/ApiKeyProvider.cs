using API.Comum.Models;
using API.Comum.Provider.Interfaces;
using API.Comum.Utils;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;

namespace API.Comum.Provider.Implementations;

public sealed class ApiKeyProvider : IApiKeyProvider
{
    private readonly TableClient _tableClient;

    public ApiKeyProvider(IConfiguration configuration)
    {
        var connectionString = configuration["AzureTable:ConnectionString"];
        var tableName = configuration["AzureTable:TableName"];
        _tableClient = new TableClient(connectionString, tableName);
        _tableClient.CreateIfNotExists();
    }

    public async Task<IEnumerable<ApiKeyEntity>> GetAllAsync()
    {
        var entities = _tableClient.QueryAsync<ApiKeyEntity>();
        var result = new List<ApiKeyEntity>();
        await foreach (var entity in entities)
        {
            result.Add(entity);
        }
        return result;
    }

    public async Task<ApiKeyEntity?> GetAsync(string licenseId, string keyId)
    {
        try
        {
            var response = await _tableClient.GetEntityAsync<ApiKeyEntity>(licenseId, keyId);
            return response.Value;
        }
        catch (Azure.RequestFailedException)
        {
            return null;
        }
    }

    public async Task<ApiKeyEntity?> FindByHashAsync(string licenseId, string sha256Hash)
    {
        var query = _tableClient.QueryAsync<ApiKeyEntity>(e =>
            e.PartitionKey == licenseId && e.ApiSha256Hash == sha256Hash);

        await foreach (var entity in query)
        {
            return entity;
        }
        return null;
    }

    public async Task AddAsync(ApiKeyEntity entity)
    {
        await _tableClient.AddEntityAsync(entity);
    }

    public async Task UpdateAsync(ApiKeyEntity entity)
    {
        await _tableClient.UpdateEntityAsync(entity, entity.ETag, TableUpdateMode.Replace);
    }

    public async Task<ApiKeyEntity?> FindByApiKeyAsync(string apiKey)
    {
        var hash = SecurityUtils.ComputeSha256(apiKey);

        var query = _tableClient.QueryAsync<ApiKeyEntity>(e => e.ApiSha256Hash == hash);

        await foreach (var entity in query)
        {
            return entity;
        }
        return null;
    }
}