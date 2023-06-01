namespace MusicLab.Repository.Models
{
    public class PlaylistSong
    {
        public int PlaylistId { get; set; }
        public int SongId { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual Playlist Playlist { get; set; }
        public virtual Song Song { get; set; }
    }
}
