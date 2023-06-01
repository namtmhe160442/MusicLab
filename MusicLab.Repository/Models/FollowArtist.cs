namespace MusicLab.Repository.Models
{
    public class FollowArtist
    {
        public string Username { get; set; }
        public int ArtistId { get; set; }
        public DateTime FollowDate { get; set; }
        public virtual User User { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
