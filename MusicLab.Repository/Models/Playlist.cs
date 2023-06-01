using System.ComponentModel.DataAnnotations;

namespace MusicLab.Repository.Models
{
    public class Playlist
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Username { get; set; } = null!;
        public virtual User User { get; set; }
        public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; }
    }
}
