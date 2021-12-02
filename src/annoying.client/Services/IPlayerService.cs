using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Extensions;

namespace annoying.client.Services
{
    public interface IPlayerService
    {
        public Task<IEnumerable<Player>> GetPlayers();
        public Task<bool> AddPlayer(Player player);
    }
    public class PlayerService : IPlayerService
    {
        private readonly HttpClient _client;

        public PlayerService(HttpClient client)
        {
            _client = client;
            //_client.BaseAddress = 
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            var response = await _client.GetAsync("api/football");
            var players = await response.ReadContentAs<List<Player>>();
            // return await _dbContext.Players.ToListAsync();
            return players;
        }

        public async Task<bool> AddPlayer(Player player)
        {
            if (player == null)
            {
                throw new NullReferenceException(nameof(player));
            }

            var response = await _client.PostAsJson("api/football", player);
            var result = await response.ReadContentAs<bool>();
            return result;
        }
    }
}