using API.Comum.Models;
using APIKEY.Crudes.DTOs;

namespace APIKEY.Crudes.Services.Interfaces;

public interface IApiKeyService
{
    Task<IEnumerable<ApiKeyEntity>> GetAllAsync();
    Task<ApiKeyDto> GenerateAsync(string licenseXd, string email, DateTimeOffset validUntil);
    Task<bool> ValidateAsync(string licenseXd, string apiKey);
    Task RevokeAsync(string licenseXd, string keyId);
}
