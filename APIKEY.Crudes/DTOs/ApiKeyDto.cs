namespace APIKEY.Crudes.DTOs;

public record ApiKeyDto(
        string LicenseXd,
        string apiKeyId,
        string apiKey,
        string Token,
        DateTimeOffset ValidUntil,
        DateTimeOffset CreatedAt
    );

