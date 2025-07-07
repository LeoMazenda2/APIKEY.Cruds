using API.Comum.Models;

namespace API.Comum.Provider.Interfaces;

public interface IApiKeyProvider
{
    Task<IEnumerable<ApiKeyEntity>> GetAllAsync();
    Task<ApiKeyEntity?> GetAsync(string licenseId, string keyId);
    Task<ApiKeyEntity?> FindByHashAsync(string licenseId, string sha256Hash);
    Task AddAsync(ApiKeyEntity entity);
    Task UpdateAsync(ApiKeyEntity entity);
}
