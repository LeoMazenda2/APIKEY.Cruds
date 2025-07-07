using API.Comum.Models;
using API.Comum.Provider.Interfaces;
using APIKEY.Crudes.DTOs;
using APIKEY.Crudes.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

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
        public async Task<ApiKeyDto> GenerateAsync(string licenseXd, string email, DateTimeOffset validUntil)
        {
            string apiKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)); // te Api Key
            string apiKeyId = Guid.NewGuid().ToString("N"); // ID to Api Key 
            string hash = ComputeSha256(apiKey); // api key hash 
            string token = Guid.NewGuid().ToString("N"); // to generete token

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

            return new ApiKeyDto(licenseXd, apiKeyId, apiKey, token, validUntil, entity.CreatedAt);
        }

        public async Task<bool> ValidateAsync(string licenseId, string plainKey)
        {
            string hash = ComputeSha256(plainKey);
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

        private static string ComputeSha256(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }
    }
}
