using API.Comum.Models;
using API.Comum.Provider.Interfaces;
using API.Comum.Utils;
using APIKEY.Crudes.DTOs;
using APIKEY.Crudes.Services.Interfaces;
using System.Security.Cryptography;

namespace APIKEY.Crudes.Services
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly IApiKeyProvider _porovider;

        public ApiKeyService(IApiKeyProvider repo) => _porovider = repo;

        public async Task<IEnumerable<ApiKeyEntity>> GetAllAsync()
        {
            return await _porovider.GetAllAsync();
        }

        public async Task<ApiKeyDto> GenerateAsync(string licenseXd, string email,DateTimeOffset validUntil)
        {
            var buffer = RandomNumberGenerator.GetBytes(32);

            var apiKey = Convert.ToBase64String(buffer)
                .Replace("=", "")
                .Replace("+", "")
                .Replace("/", "");

            string apiKeyId = Guid.NewGuid().ToString("N");

            string hash = SecurityUtils.ComputeSha256(apiKey);

            string token = Guid.NewGuid().ToString("N");

            var entity = new ApiKeyEntity
            {
                PartitionKey = licenseXd,
                RowKey = apiKeyId,
                Email = email,
                ApiSha256Hash = hash,
                Token = token,
                ValidUntil = validUntil,
                CreatedAt = DateTimeOffset.UtcNow,
                IsActive = true
            };

            await _porovider.AddAsync(entity);

            return new ApiKeyDto(licenseXd, apiKey, token, validUntil);
        }

        public async Task<bool> ValidateAsync(string licenseId, string apiKey)
        {
            string hash = SecurityUtils.ComputeSha256(apiKey);
            var entity = await _porovider.FindByHashAsync(licenseId, hash);
            return entity is not null && entity.IsActive && entity.ValidUntil > DateTimeOffset.UtcNow;
        }

        public async Task RevokeAsync(string licenseId, string keyId)
        {
            var entity = await _porovider.GetAsync(licenseId, keyId);
            if (entity is null) return;
            entity.IsActive = false;
            await _porovider.UpdateAsync(entity);
        }      
    }
}
