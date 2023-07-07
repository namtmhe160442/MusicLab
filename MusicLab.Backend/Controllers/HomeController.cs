using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLab.Backend.Services.Interfaces;
using MusicLab.Repository;
using MusicLab.Repository.Models;
using MusicLab.Repository.Repositories.Interfaces;

namespace MusicLab.Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IAWSS3Service awsS3Service;
        private IPlaylistRepository playlistRepository;
        private IPlayHistoryRepository playHistoryRepository;
        private ISongRepository songRepository;

        public HomeController(IAWSS3Service aWSS3Service, IUnitOfWork unitOfWork)
        {
            awsS3Service = aWSS3Service;
            playlistRepository = unitOfWork.PlaylistRepository;
            playHistoryRepository = unitOfWork.PlayHistoryRepository;
            songRepository = unitOfWork.SongRepository;
        }

        [Authorize]
        [HttpGet("/api/get-all-playlists")]
        public async Task<List<Playlist>> GetAllPlaylists(string username)
        {
            return await playlistRepository.Find(x => x.Username.Equals(username)).ToListAsync().ConfigureAwait(false);
        }

        [Authorize]
        [HttpGet("/api/get-top-6-playlists")]
        public async Task<List<Playlist>> GetTop6Playlists(string username)
        {
            return await playlistRepository.Find(x => x.Username.Equals(username))
                                           .OrderBy(x => x.CreatedDate)
                                           .Take(6).ToListAsync().ConfigureAwait(false);
        }

        [Authorize]
        [HttpGet("/api/get-top-6-last-played-songs")]
        public async Task<List<Song>> GetTop6LastPlayedSongs(string username)
        {
            return await playHistoryRepository.GetTop6LastPlayedSongs(username).ConfigureAwait(false);
        }

        [HttpGet("/api/get-trending-songs")]
        public async Task<List<Song>> GetTrendingSongs()
        {
            return await songRepository.Find(x => x.DatePublished >= DateTime.Now.AddDays(-10))
                                       .OrderBy(x => x.NumberOfListen)
                                       .Take(6)
                                       .ToListAsync().ConfigureAwait(false);
        }

        [Authorize]
        [HttpGet("/api/get-top-6-recommended-songs")]
        public async Task<List<Song>> GetTop6RecommendedSongs(string username)
        {
            var songs = await songRepository.GetAllRecommendedSongs(username).ConfigureAwait(false);
            return songs.Take(6).ToList();
        }

        [Authorize]
        [HttpGet("/api/get-all-recommended-songs")]
        public async Task<List<Song>> GetAllRecommendedSongs(string username)
        {
            return await songRepository.GetAllRecommendedSongs(username).ConfigureAwait(false);
        }

        [HttpGet("/api/test")]
        public void Test()
        {
            InitDatabase.DummyData();
        }
    }
}
