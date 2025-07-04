using API.Comum.Provider.Interfaces;
using System.Security.Cryptography;
using System.Text;
using Azure.Data.Tables;

namespace API.Comum.Provider.Implementations;

public class AzuriteApiKeyProvider : IApiKeyProvider
{
    private readonly TableClient _tableClient;

    public AzuriteApiKeyProvider(string connectionString, string tableName)
    {
        _tableClient = new TableClient(connectionString, tableName);
        _tableClient.CreateIfNotExists();
    }

    public async Task<string> GenerateAndSaveApiKeyAsync(string licenseKey, string createdBy, string remarks, DateTime? validUntil = null)
    {
        var apiKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        var apiKeyHash = ComputeSha256Hash(apiKey);

        var entity = new TableEntity("XdSobaApiKeys", Guid.NewGuid().ToString())
        {
            { "ApiKeyHash", apiKeyHash },
            { "LicenseKey", licenseKey },
            { "CreatedAt", DateTime.UtcNow },
            { "ValidUntil", validUntil },
            { "IsActive", true },
            { "CreatedBy", createdBy },
            { "Remarks", remarks }
        };

        await _tableClient.AddEntityAsync(entity);
        return apiKey;
    }

    private static string ComputeSha256Hash(string rawData)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        return Convert.ToBase64String(bytes);
    }
}
