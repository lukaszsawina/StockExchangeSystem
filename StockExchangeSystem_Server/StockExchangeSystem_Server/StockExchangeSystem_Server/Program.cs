using CurrencyExchangeLibrary;
using CurrencyExchangeLibrary.Data;
using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StockExchangeSystem_Server.PeriodicServices;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddSingleton<PeriodicHostedService>();
builder.Services.AddScoped<ICryptoRepository, CryptoRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAPIKeyLogic, APIKeyLogic>();
builder.Services.AddScoped<IRefreshLogic, RefreshLogic>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddHostedService(
    provider => provider.GetRequiredService<PeriodicHostedService>());


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});



app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("App is running");
    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "The application failed to start correctyly :(");
}
finally
{
    Log.CloseAndFlush();
}


