using System.Threading;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using yourmusic.Models;

namespace yourmusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicControllers : ControllerBase
    {
        private readonly ILogger<Music> _logger;

        public MusicControllers(ILogger<Music> logger) {
            this._logger = logger;
        }

        [HttpGet]
        public ICollection<Music> GetMusics()
        {
            return null;
        }
    }
}