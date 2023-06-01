using System.ComponentModel.DataAnnotations;

namespace MusicLab.Repository.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Avatar { get; set; }
        public bool Gender { get; set; }

        public virtual ICollection<FollowArtist> FollowArtists { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<PlayHistory> PlayHistories { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}
