using Microsoft.EntityFrameworkCore;
using MusicLab.Repository.Models;
using MusicLab.Repository.Repositories.Interfaces;

namespace MusicLab.Repository.Repositories
{
    public class PlayHistoryRepository : BaseRepository<PlayHistory>, IPlayHistoryRepository
    {
        private readonly MusicLabContext _context;
        public PlayHistoryRepository(MusicLabContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Song>> GetTop6LastPlayedSongs(string username)
        {
            return await _context.PlayHistories.Join(_context.Songs, t1 => t1.SongId, t2 => t2.Id,
                                                (t1,t2) => new {History = t1, Song = t2})
                                                .Where(x => x.History.Username == username)
                                                .OrderByDescending(x => x.History.PlayedDate)
                                                .Take(6).Select(x => x.Song)
                                                .ToListAsync().ConfigureAwait(false);
        }
    }
}
