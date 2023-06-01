namespace MusicLab.Repository.Models
{
    public class PlayHistory
    {
        public string Username { get; set; }
        public int SongId { get; set; }
        public DateTime PlayedDate { get; set; }
        public long Duration { get; set; }
        public virtual User User { get; set; }
        public virtual Song Song { get; set; }
    }
}
