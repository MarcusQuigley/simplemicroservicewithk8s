using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using annoying.client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace annoying.client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPlayerService _playerService;

        public DateTime NowTime { get; set; }
        public IEnumerable<Player> Players { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IPlayerService playerService)
        {
            _logger = logger;
            _playerService = playerService;
        }

        public async Task OnGet()
        {
            NowTime = DateTime.Now;
            Players = await _playerService.GetPlayers();

        }
    }
}
