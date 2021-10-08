using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using yourmusic.Models;
using yourmusic.Context;
using Microsoft.AspNetCore.Authorization;

namespace yourmusic.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MusicControllers : ControllerBase
    {
        private readonly ILogger<Music> _logger;
        private ContextDatabase _db;

        public MusicControllers(ILogger<Music> logger, ContextDatabase db) 
        {
            this._logger = logger;
            this._db = db;
        }

        [HttpGet]
        [Route("{id}")]
        public Music GetMusicById(int id)
        {
            return _db.Musics.Find(id);
        }

        [HttpGet]
        public IEnumerable<Music> GetMusics(string name = null, string artist = null, string releaseDate = null) 
        {
            DateTime time;
            bool check = DateTime.TryParse(releaseDate, out time);
            List<Music> list = _db.Musics
                 .Where(music =>
                 (name == null || music.Name.ToLower().Contains(name.ToLower())) &&
                 (artist == null || music.Artist.ToLower().Contains(artist.ToLower())) &&
                 (releaseDate == null || (check && music.ReleaseDate == time))
                 ).ToList();

            return list;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult PostMusic(Music music) 
        {
            music.Id = 0;
            _db.Musics.Add(music);
            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteMusic(int id)
        {
            Music music = _db.Musics.Find(id);

            if(music != null) 
            {
                _db.Musics.Remove(music);
                _db.SaveChanges();
                return Ok($"{music.Name} is deleted");
            }

            return NoContent();
        }


        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateMusic(int id, Music music)
        {
            if(_db.Musics.Find(music.Id) == null) {
                return NoContent();
            }
            music.Id = id;
            _db.Musics.Update(music);
            _db.SaveChanges();

            return Ok($"{music.Id} is updated");
        }
    }
}