using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Adiciona Ocelot
builder.Services.AddOcelot();

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