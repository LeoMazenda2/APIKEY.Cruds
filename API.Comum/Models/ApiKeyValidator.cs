using API.Comum.Models;

namespace API.Comum.Validation;

public static class ApiKeyValidator
{
    public static bool Exists(ApiKeyEntity? entity) => entity != null;

    public static bool IsActive(ApiKeyEntity entity) => entity.IsActive;

    public static bool IsValid(ApiKeyEntity entity) => entity.ValidUntil > DateTimeOffset.UtcNow;

    public static ApiKeyValidationResult Validate(ApiKeyEntity? entity)
    {
        if (entity == null)
            return ApiKeyValidationResult.NotFound;

        if (!entity.IsActive)
            return ApiKeyValidationResult.Inactive;

        if (entity.ValidUntil <= DateTimeOffset.UtcNow)
            return ApiKeyValidationResult.Expired;

        return ApiKeyValidationResult.Valid;
    }
}

public enum ApiKeyValidationResult
{
    Valid,
    NotFound,
    Inactive,
    Expired
}