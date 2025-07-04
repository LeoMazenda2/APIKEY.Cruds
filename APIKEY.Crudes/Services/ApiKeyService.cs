using API.Comum.Provider.Interfaces;

namespace APIKEY.Crudes.Services
{
    public class ApiKeyService
    {
        private readonly IApiKeyProvider _provider;
        public ApiKeyService(IApiKeyProvider provider)
        {
            _provider = provider;
        }

        public Task<string> GenerateApiKeyAsync(string licenseKey, string createdBy, string remarks, DateTime? validUntil = null)
            => _provider.GenerateAndSaveApiKeyAsync(licenseKey, createdBy, remarks, validUntil);
    }
}
