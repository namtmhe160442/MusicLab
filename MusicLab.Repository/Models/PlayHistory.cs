using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLab.Repository.Models
{
    [Table("PlayHistory")]
    public class PlayHistory
    {
        public string Username { get; set; } = null!;
        public int SongId { get; set; }
        public DateTime PlayedDate { get; set; }
        public long Duration { get; set; }
        public virtual User User { get; set; }
        public virtual Song Song { get; set; }
    }
}
