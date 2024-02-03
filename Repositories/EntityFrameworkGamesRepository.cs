using GameStore.Data;
using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repositories
{
    public class EntityFrameworkGamesRepository : IGamesRepository
    {
        private readonly GameStoreContext _dbContext;
        private readonly ILogger<EntityFrameworkGamesRepository> _logger;

        public EntityFrameworkGamesRepository(
            GameStoreContext dbContext,
            ILogger<EntityFrameworkGamesRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _dbContext.Games
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Game?> GetAsync(int id)
        {
            return await _dbContext.Games.FindAsync(id);
        }

        public async Task CreateAsync(Game game)
        {
            _dbContext.Games.Add(game);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Game updatedGame)
        {
            _dbContext.Games.Add(updatedGame);

            await _dbContext.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            await _dbContext.Games
                  .Where(game => game.Id == id)
                  .ExecuteDeleteAsync();
        }
    }
}
