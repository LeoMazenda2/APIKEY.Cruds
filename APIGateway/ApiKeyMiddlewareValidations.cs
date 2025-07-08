namespace APIGateway;

public class ApiKeyMiddlewareValidations
{
    private readonly RequestDelegate _next;
    private const string APIKEY = "X-API-KEY";
    private const string VALID_API_KEY = "sua-chave-secreta-aqui"; // Troque pela sua chave

    public ApiKeyMiddlewareValidations(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey) ||
            extractedApiKey != VALID_API_KEY)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key inválida ou ausente.");
            return;
        }

        await _next(context);
    }
}
