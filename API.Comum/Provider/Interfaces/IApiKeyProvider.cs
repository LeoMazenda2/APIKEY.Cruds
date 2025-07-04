namespace API.Comum.Provider.Interfaces;

public interface IApiKeyProvider
{
    Task<string> GenerateAndSaveApiKeyAsync(string licenseKey, string createdBy, string remarks, DateTime? validUntil = null);
}
