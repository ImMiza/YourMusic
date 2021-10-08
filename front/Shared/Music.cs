using System;
using System.ComponentModel.DataAnnotations;

namespace yourmusic.Models
{
    public class Music
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Artist { get; set; }
        
        public string UrlImage { get; set; }

        public string UrlMusic { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}