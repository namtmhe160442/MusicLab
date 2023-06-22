using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLab.Repository.Models
{
    [Table("Artist")]
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
