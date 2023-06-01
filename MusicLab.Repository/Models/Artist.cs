using System.ComponentModel.DataAnnotations;

namespace MusicLab.Repository.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Biography { get; set; } = null!;
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<FollowArtist> FollowArtists { get; set; }
    }
}
