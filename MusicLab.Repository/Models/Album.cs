using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLab.Repository.Models
{
    [Table("Album")]
    public class Album
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public DateTime DatePublished { get; set; }
        public int ArtistId { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
