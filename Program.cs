using GameStore.Data;
using GameStore.Endpoints;
using GameStore.Repositories;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient()
//builder.Services.AddScoped()
//builder.Services.AddSingleton()
builder.Services.AddSingleton<IGamesRepository, InMemoryGamesRepository>();

// Best practice to store variables for conn string
// 1) Command line args
// 2) Environment variables
// 3) Cloud Service
// End 
var conn = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddSqlServer<GameStoreContext>(conn);

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
