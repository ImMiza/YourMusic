using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace yourmusic.Models
{
    
    public class Playlist
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string UrlImage { get; set; }

        [Required]
        public ICollection<Music> Musics { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}