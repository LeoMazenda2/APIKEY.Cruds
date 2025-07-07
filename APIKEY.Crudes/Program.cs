using API.Comum.Provider.Implementations;
using API.Comum.Provider.Interfaces;
using APIKEY.Crudes.Data;
using APIKEY.Crudes.Repositories.Implementations;
using APIKEY.Crudes.Repositories.Interfaces;
using APIKEY.Crudes.Services;
using APIKEY.Crudes.Services.Interfaces;
using Azure.Data.Tables;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<TableServiceClient>(_ =>
    new TableServiceClient(builder.Configuration.GetSection("AzureTable")["ConnectionString"]));

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ICarroRepository, CarroRepository>();

builder.Services.AddScoped<IApiKeyProvider, ApiKeyProvider>();
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
