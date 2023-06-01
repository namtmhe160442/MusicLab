namespace MusicLab.Repository.Models
{
    public class Favourite
    {
        public string Username { get; set; }
        public int SongId { get; set; }
        public DateTime LikedDate { get; set; }
        public virtual User User { get; set; }
        public virtual Song Song { get; set; }
    }
}
