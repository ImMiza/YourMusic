using System;
using System.ComponentModel.DataAnnotations;

namespace yourmusic.Models
{
    public class Music
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Artist { get; set; }
        
        public string UrlImage { get; set; }

        [Required]
        public string UrlMusic { get; set; }

        [Required]
        public DateTime RealeaseDate { get; set; }
    }
}