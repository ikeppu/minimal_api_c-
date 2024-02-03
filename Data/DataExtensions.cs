using GameStore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    public static class DataExtensions
    {
        public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

                await db.Database.MigrateAsync();
                var logger = serviceProvider.GetRequiredService<ILoggerFactory>()
                    .CreateLogger("DB Initializer");

                logger.LogInformation(5, "The database is ready");
            }

        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Best practice to store variables for conn string
            // 1) Command line args
            // 2) Environment variables
            // 3) Cloud Service
            // End 
            var conn = configuration.GetConnectionString("GameStore");

            //builder.Services.AddTransient()
            //builder.Services.AddScoped()
            //builder.Services.AddSingleton()
            services.AddSqlServer<GameStoreContext>(conn)
                .AddScoped<IGamesRepository, EntityFrameworkGamesRepository>();

            return services;
        }
    }
}
