using CurrencyExchangeLibrary;
using CurrencyExchangeLibrary.Data;
using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICryptoRepository, CryptoRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAPIKeyLogic, APIKeyLogic>();
builder.Services.AddScoped<IRefreshLogic, RefreshLogic>();
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

void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
{
    using (var scope = app.Services.CreateScope())
    {
        var refresher = scope.ServiceProvider.GetRequiredService<IRefreshLogic>();

        refresher.Refresh();
    }
}

System.Timers.Timer timer = new System.Timers.Timer();
timer.Interval = 300000;
timer.Elapsed += timer_Elapsed;
timer.Start();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
