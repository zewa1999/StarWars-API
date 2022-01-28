using StarWarsAPI.DataProvider;
using StarWarsAPI.Server;
using StarWarsAPI.Server.Strategy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<ILogger>(svc => svc.GetRequiredService<ILogger<DataProvider>>());
builder.Services.AddTransient<IDataProvider, DataProvider>();
builder.Services.AddTransient<IStrategyGetCaller, StrategyGetCaller>();
builder.Services.AddSingleton<IIdentityMap, IdentityMap>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();