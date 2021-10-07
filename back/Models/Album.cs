using System.ComponentModel.DataAnnotations;
namespace yourmusic.Models
{
    public class Album : Playlist
    {
        [Required]
        public string Artist { get; set; }
    }
}