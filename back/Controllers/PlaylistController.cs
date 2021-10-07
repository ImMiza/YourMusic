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
    public class PlaylistControllers : ControllerBase
    {
        private readonly ILogger<Music> _logger;
        private ContextDatabase _db;

        public PlaylistControllers(ILogger<Music> logger, ContextDatabase db) 
        {
            this._logger = logger;
            this._db = db;
        }

        [HttpGet]
        [Route("{id}")]
        public Playlist GetPlaylistsById(int id)
        {
            Playlist playlist = _db.Playlists.Find(id);
            if(playlist == null) {
                return null;
            }

            _db.Entry(playlist).Collection(pl => pl.Musics).Load();
            return playlist;
        }

        [HttpGet]
        public IEnumerable<Playlist> GetPlaylists(string name = null) 
        {

            List<Playlist> playlists = _db.Playlists.Where(playlist => 
                (name == null || playlist.Name.Contains(name))
            ).ToList();

            foreach (var pl in playlists)
            {
                _db.Entry(pl).Collection(p => p.Musics).Load();
            }

            return playlists;
        }

        [HttpPost]
        public IActionResult PostPlayList(Playlist playlist) 
        {
            Playlist pl = new Playlist()
            {
                Name = playlist.Name,
                Description = playlist.Description,
                ReleaseDate = playlist.ReleaseDate,
                UrlImage = playlist.UrlImage,
                Musics = playlist.Musics
            };
            _db.Playlists.Add(pl);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdatePlaylist(int id, Playlist playlist)
        {
            Playlist pl = _db.Playlists.Find(id);
            if(pl == null) {
                return NoContent();
            }

            pl = new Playlist()
            {
                Id = id,
                Name = playlist.Name,
                Description = playlist.Description,
                ReleaseDate = playlist.ReleaseDate,
                UrlImage = playlist.UrlImage,
                Musics = playlist.Musics
            };

            _db.Playlists.Update(pl);
            _db.SaveChanges();

            return Ok($"{playlist.Id} is updated");
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletePlayList(int id) {
            Playlist pl = _db.Playlists.Find(id);

            if(pl != null) 
            {
                _db.Playlists.Remove(pl);
                _db.SaveChanges();
                return Ok($"{pl.Name} is deleted");
            }

            return NoContent();
        }

        [HttpPost]
        [Route("{idPlaylist}/{idMusic}")]
        public IActionResult PostMusicInPlaylist(int idPlaylist, int idMusic)
        {
            Playlist playlist = _db.Playlists.Find(idPlaylist);
            if(playlist == null) {
                return NoContent();
            }

            Music music = _db.Musics.Find(idMusic);
            if(music == null) {
                return NoContent();
            }

            playlist.Musics = playlist.Musics == null ? new List<Music>() : playlist.Musics;
            playlist.Musics.Add(music);
            _db.Playlists.Update(playlist);
            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("{idPlaylist}/{idMusic}")]
        public IActionResult DeleteMusicInPlaylist(int idPlaylist, int idMusic)
        {
            Playlist playlist = _db.Playlists.Find(idPlaylist);
            if(playlist == null) {
                return NoContent();
            }

            Music music = _db.Musics.Find(idMusic);
            if(music == null) {
                return NoContent();
            }

            if(playlist.Musics != null) {
                playlist.Musics.Remove(music);
                _db.Playlists.Update(playlist);
                _db.SaveChanges();
            }

            return Ok();
        }
    }
}