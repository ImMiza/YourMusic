using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using yourmusic.Models;
using yourmusic.Context;

namespace yourmusic.Controllers
{
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
            Music music = _db.Musics.Find(id);
            return music;
        }

        [HttpGet]
        public IEnumerable<Music> GetMusics(string name = null, string artist = null, string releaseDate = null) 
        {
            DateTime time;
            bool check = DateTime.TryParse(releaseDate, out time);
            List<Music> list = _db.Musics
                 .Where(music =>
                 (name == null || music.Name.Equals(name)) &&
                 (artist == null || music.Artist.Equals(artist)) &&
                 (releaseDate == null || (check && music.ReleaseDate == time))
                 ).ToList();

            return list;
        }

        [HttpPost]
        public IActionResult PostMusic(string name, string artist, string urlMusic, string releaseDate, string urlImage = "") 
        {
            DateTime time;
            bool check = DateTime.TryParse(releaseDate, out time);
            
            if(!check)
            {
                return Unauthorized("releaseDate is invalid. Format (YYYY-MM-DD)");
            }

            Music music = new Music()
            {
                Name = name,
                Artist = artist,
                UrlImage = urlImage,
                UrlMusic = urlMusic,
                ReleaseDate = time
            };
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
                return Ok($"{music.Name} is deleted");
            }

            return NoContent();
        }


        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateMusic(int id, string name = null, string artist = null, string urlImage = null, string urlMusic = null, string releaseDate = null)
        {
            Music music = _db.Musics.Find(id);
            if(music == null) {
                return NoContent();
            }

            DateTime time;
            bool check = DateTime.TryParse(releaseDate, out time);

            music.Name = name != null ? name : music.Name;
            music.Artist = artist != null ? artist : music.Artist;
            music.UrlImage = urlImage != null ? urlImage : music.UrlImage;
            music.UrlMusic = urlMusic != null ? urlMusic : music.UrlMusic;
            music.ReleaseDate = releaseDate != null && check ? time : music.ReleaseDate;

            _db.Musics.Update(music);
            _db.SaveChanges();

            return Ok($"{id} is updated");
        }
    }
}