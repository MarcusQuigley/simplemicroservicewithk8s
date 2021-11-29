using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace annoying.api.Services
{
    public interface IPlayerService
    {
        public Task<IEnumerable<Player>> GetPlayers();
    }
    public class PlayerService : IPlayerService
    {
        private readonly ClientDbContext _dbContext;

        public PlayerService(ClientDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            return await _dbContext.Players.ToListAsync();
        }
    }
}