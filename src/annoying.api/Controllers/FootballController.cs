using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using annoying.api.Services;

namespace annoying.api.Controllers
{
    [ApiController]
    [Route("api/football")]
    public class FootballController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly ILogger<FootballController> _logger;

        public FootballController(ILogger<FootballController> logger, IPlayerService playerService)
        {
            _logger = logger;
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<ActionResult<Player>> GetPlayers()
        {
            var players = await _playerService.GetPlayers();
            return players != null ? Ok(players) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddPlayer(Player player)
        {
            var result = await _playerService.AddPlayer(player);
            return Ok(result);
        }
    }
}
