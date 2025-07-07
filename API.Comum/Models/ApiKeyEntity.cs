using Azure;
using Azure.Data.Tables;

namespace API.Comum.Models;

public class ApiKeyEntity : ITableEntity
{
    public string PartitionKey { get; set; } = default!;  // LicenseId
    public string RowKey { get; set; } = default!;  // KeyId (GUID)

    public string Email { get; set; } = default!;  // Contact e‑mail
    public string ApiSha256Hash { get; set; } = default!;  // Hash da chave
    public string Token { get; set; } = default!;  // Token de uso
    public DateTimeOffset ValidUntil { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public ETag ETag { get; set; } = ETag.All;
    public DateTimeOffset? Timestamp { get; set; }
}
