namespace APIKEY.Crudes.DTOs;

public record ApiKeyDto (string LicenseXd,
    string apiKey,
    string Token,
    DateTimeOffset ValidUntil);

