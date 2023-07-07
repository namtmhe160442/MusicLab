using Microsoft.EntityFrameworkCore;
using MusicLab.Repository.Models;
using MusicLab.Repository.Repositories.Interfaces;

namespace MusicLab.Repository.Repositories
{
    public class SongRepository : BaseRepository<Song>, ISongRepository
    {
        private readonly MusicLabContext _context;
        public SongRepository(MusicLabContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Song>> GetAllRecommendedSongs(string username)
        {
            var favouriteSongs = await _context.Favourites.Join(_context.Songs, t1 => t1.SongId, t2 => t2.Id,
                                                (t1, t2) => new { Favourite = t1, Song = t2 })
                                                .Where(x => x.Favourite.Username == username)
                                                .Select(x => x.Song.Id).ToListAsync().ConfigureAwait(false);
            var genres = await _context.SongCategories.Join(_context.Categories, t1 => t1.CategoryId, t2 => t2.Id,
                                                (t1, t2) => new { SongCategories = t1, Category = t2 })
                                                .ToListAsync().ConfigureAwait(false);
            var songIds = genres.Where(x => favouriteSongs.Contains(x.SongCategories.SongId) && x.Category.IsGenre)
                                                .Select(x => x.SongCategories.SongId);
            var songs = await _context.Songs.Where(x => songIds.Contains(x.Id)).ToListAsync().ConfigureAwait(false);
            return songs;
        }
    }
}
