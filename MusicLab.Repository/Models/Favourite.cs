using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLab.Repository.Models
{
    [Table("Favourite")]
    public class Favourite
    {
        public string Username { get; set; } = null!;
        public int SongId { get; set; }
        public DateTime LikedDate { get; set; }
        public virtual User User { get; set; }
        public virtual Song Song { get; set; }
    }
}
