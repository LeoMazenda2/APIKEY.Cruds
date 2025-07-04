using API.Comum.Provider.Implementations;
using API.Comum.Provider.Interfaces;
using APIKEY.Crudes.Data;
using APIKEY.Crudes.Repositories.Implementations;
using APIKEY.Crudes.Repositories.Interfaces;
using APIKEY.Crudes.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var azureConfig = builder.Configuration.GetSection("AzureTable");
builder.Services.AddSingleton<IApiKeyProvider>(
    _ => new AzuriteApiKeyProvider(
        azureConfig["ConnectionString"],
        azureConfig["TableName"]
    )
);
builder.Services.AddScoped<ApiKeyService>();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ICarroRepository, CarroRepository>();


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
