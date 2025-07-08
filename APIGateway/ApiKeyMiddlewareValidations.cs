using API.Comum.Provider.Interfaces;
using API.Comum.Validation;

namespace APIGateway;

public class ApiKeyMiddlewareValidations
{
    private readonly RequestDelegate _next;
    private readonly IApiKeyProvider _apiKeyProvider;
    private const string APIKEY = "X-API-KEY";
    //private const string TOKEN = "X-TOKEN";

    public ApiKeyMiddlewareValidations(RequestDelegate next, IApiKeyProvider apiKeyProvider)
    {
        _next = next;
        _apiKeyProvider = apiKeyProvider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key ausente.");
            return;
        }

        // Busca a entidade usando a API Key original (o provider calcula o hash)
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