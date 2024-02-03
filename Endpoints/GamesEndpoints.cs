using GameStore.Authorization;
using GameStore.Entities;
using GameStore.Repositories;

namespace GameStore.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndpointName = "GetGame";

        public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes
                .NewVersionedApi()
                .MapGroup("/v{version:apiVersion}/games")
                .HasApiVersion(1.0)
                .HasApiVersion(2.0)
                .WithParameterValidation();

            group.MapGet("", async (IGamesRepository repository, ILoggerFactory loggerFactory) =>
            {
                return Results.Ok((await repository.GetAllAsync())
                    .Select(game => game.AsDtoV1()));
            })
            .MapToApiVersion(1.0);

            group.MapGet("", async (IGamesRepository repository, ILoggerFactory loggerFactory) =>
            {
                return Results.Ok((await repository.GetAllAsync())
                    .Select(game => game.AsDtoV2()));
            })
            .MapToApiVersion(2.0);

            group.MapGet("/{id}", async (IGamesRepository repository, int id) =>
            {
                Game? game = await repository.GetAsync(id);
                return game is not null ? Results.Ok(game.AsDtoV1()) : Results.NotFound();
            }).WithName(GetGameEndpointName)
              .RequireAuthorization(Policies.ReadAccess)
              .MapToApiVersion(1.0);

            group.MapPost("", async (IGamesRepository repository, CreateGameDto gameDto) =>
            {
                Game game = new()
                {
                    Name = gameDto.Name,
                    Genre = gameDto.Genre,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    ImageUri = gameDto.ImageUri
                };

                await repository.CreateAsync(game);

                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
            }).RequireAuthorization(Policies.WriteAccess).MapToApiVersion(1.0);

            group.MapPut("/{id}", async (IGamesRepository repository, int id, UpdateGameDto updatedGameDto) =>
            {
                Game? existingGame = await repository.GetAsync(id);

                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                // Use mapping instead
                existingGame.Name = updatedGameDto.Name;
                existingGame.Genre = updatedGameDto.Genre;
                existingGame.Price = updatedGameDto.Price;
                existingGame.ReleaseDate = updatedGameDto.ReleaseDate;
                existingGame.ImageUri = updatedGameDto.ImageUri;

                await repository.UpdateAsync(existingGame);

                return Results.NoContent();
            }).RequireAuthorization(Policies.WriteAccess).MapToApiVersion(1.0);

            group.MapDelete("/{id}", async (IGamesRepository repository, int id) =>
            {
                Game? game = await repository.GetAsync(id);

                if (game is not null)
                {
                    await repository.DeleteAsync(id);
                }

                return Results.NoContent();
            }).RequireAuthorization(Policies.WriteAccess).MapToApiVersion(1.0);

            return group;
        }
    }
}
