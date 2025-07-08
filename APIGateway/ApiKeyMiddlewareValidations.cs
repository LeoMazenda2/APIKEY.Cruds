using API.Comum.Provider.Interfaces;
using API.Comum.Validation;

namespace APIGateway;

public class ApiKeyMiddlewareValidations
{
    private readonly RequestDelegate _next;
    private readonly IApiKeyProvider _apiKeyProvider;
    private const string APIKEY = "X-API-KEY";
    //private const string TOKEN = "X-TOKEN";

    private static readonly string[] WhitelistPaths = new[]
    {
        "/api/publica",
        "/api/health"
    };

    private static readonly string[] WhitelistHosts = new[]
    {
        "sxsoba.com",
        "localhost:4200"
    };

    public ApiKeyMiddlewareValidations(RequestDelegate next, IApiKeyProvider apiKeyProvider)
    {
        _next = next;
        _apiKeyProvider = apiKeyProvider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var hostHeader = context.Request.Headers.Host.ToString().ToLowerInvariant();
        var originHeader = context.Request.Headers.Origin.ToString().ToLowerInvariant();

        if (WhitelistHosts.Any(h => hostHeader.Contains(h) || originHeader.Contains(h)))
        {
            await _next(context);
            return;
        }

        var path = context.Request.Path.Value?.ToLowerInvariant();
        if (path != null && WhitelistPaths.Any(p => path.StartsWith(p.ToLowerInvariant())))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key ausente.");
            return;
        }
        var entity = await _apiKeyProvider.FindByApiKeyAsync(extractedApiKey);

        var validation = ApiKeyValidator.Validate(entity);

        switch (validation)
        {
            case ApiKeyValidationResult.NotFound:
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key inválida.");
                return;
            case ApiKeyValidationResult.Inactive:
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key inativa.");
                return;
            case ApiKeyValidationResult.Expired:
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key expirada.");
                return;
            case ApiKeyValidationResult.Valid:
                // prossegue normalmente
                break;
        }

        // Se quiser validar o token, descomente e adapte:
        //if (!context.Request.Headers.TryGetValue(TOKEN, out var extractedToken))
        //{
        //    context.Response.StatusCode = 401;
        //    await context.Response.WriteAsync("Token ausente.");
        //    return;
        //}
        //if (entity.Token != extractedToken)
        //{
        //    context.Response.StatusCode = 401;
        //    await context.Response.WriteAsync("Token inválido.");
        //    return;
        //}

        await _next(context);
    }
}