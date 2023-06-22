using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLab.Repository.Models
{
    [Table("Song")]
    public class Song
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; } = null!;
        [Required]
        [Column(TypeName = "text")]
        public string Link { get; set; } = null!;
        public long Duration { get; set; }
        [Required]
        public DateTime DatePublished { get; set; }
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
        public virtual ICollection<SongCategory> SongCategories { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; }
        public virtual ICollection<PlayHistory> PlayHistories { get; set; }
    }
}
