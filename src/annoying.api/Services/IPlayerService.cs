using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace annoying.api.Services
{
    public interface IPlayerService
    {
        public Task<IEnumerable<Player>> GetPlayers();
        public Task<bool> AddPlayer(Player player);
        //public bool SaveChanges();
    }
    public class PlayerService : IPlayerService
    {
        private readonly ClientDbContext _dbContext;

        public PlayerService(ClientDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddPlayer(Player player)
        {
            await _dbContext.Players.AddAsync(player);
            return await SaveChanges();
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            return await _dbContext.Players.ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync() >= 0;
        }
    }
}