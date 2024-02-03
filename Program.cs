using GameStore.Authorization;
using GameStore.Cors;
using GameStore.Data;
using GameStore.Endpoints;
using GameStore.ErrorHandling;
using GameStore.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
// AUTH
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddGameStoreAuthorization();
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new(1.0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Services.AddGameStoreCors(builder.Configuration);

//builder.Logging.AddJsonConsole();

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.ConfigureExceptionHandler());
app.UseMiddleware<RequestTimingMiddleware>();

// Automatically apply migration to the database
await app.Services.InitializeDbAsync();

//app.UseHttpLogging();
app.MapGamesEndpoints();
app.UseCors();

app.Run();
