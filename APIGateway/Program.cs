using API.Comum.Provider.Implementations;
using API.Comum.Provider.Interfaces;
using APIKEY.Crudes.Services;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var azureConfig = builder.Configuration.GetSection("AzureTable");
builder.Services.AddSingleton<IApiKeyProvider>(
    _ => new AzuriteApiKeyProvider(
        azureConfig["ConnectionString"],
        azureConfig["TableName"]
    )
);
builder.Services.AddScoped<ApiKeyService>();

// Carrega sempre o arquivo ocelot.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.AddOcelot(configuration);

// Swagger para documentação do gateway (opcional, mas útil para testes)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware do Swagger (apenas em desenvolvimento)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ocelot deve ser o último middleware antes do Run
await app.UseOcelot();

app.Run();