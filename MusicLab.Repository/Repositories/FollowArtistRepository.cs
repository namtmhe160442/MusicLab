using MusicLab.Repository.Models;
using MusicLab.Repository.Repositories.Interfaces;

namespace MusicLab.Repository.Repositories
{
    public class FollowArtistRepository : BaseRepository<FollowArtist>, IFollowArtistRepository
    {
        private readonly MusicLabContext _context;
        public FollowArtistRepository(MusicLabContext context) : base(context)
        {
            _context = context;
        }
    }
}
