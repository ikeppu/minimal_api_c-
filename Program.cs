using GameStore.Authorization;
using GameStore.Data;
using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
// AUTH
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddGameStoreAuthorization();

var app = builder.Build();

// Automatically apply migration to the database
await app.Services.InitializeDbAsync();

app.MapGamesEndpoints();

app.Run();
