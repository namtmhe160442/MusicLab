using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLab.Backend.Services.Interfaces;
using MusicLab.Repository;
using MusicLab.Repository.Models;
using MusicLab.Repository.Models.ResponseModel;
using MusicLab.Repository.Repositories.Interfaces;

namespace MusicLab.Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IAWSS3Service _awsS3Service;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IPlayHistoryRepository _playHistoryRepository;
        private readonly ISongRepository _songRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;

        public HomeController(IAWSS3Service aWSS3Service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _awsS3Service = aWSS3Service;
            _playlistRepository = unitOfWork.PlaylistRepository;
            _playHistoryRepository = unitOfWork.PlayHistoryRepository;
            _songRepository = unitOfWork.SongRepository;
            _artistRepository = unitOfWork.ArtistRepository;
            _albumRepository = unitOfWork.AlbumRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("/api/get-all-playlists")]
        public async Task<List<Playlist>> GetAllPlaylists(string username)
        {
            return await _playlistRepository.Find(x => x.Username.Equals(username)).ToListAsync().ConfigureAwait(false);
        }

        [Authorize]
        [HttpGet("/api/get-top-6-playlists")]
        public async Task<List<Playlist>> GetTop6Playlists(string username)
        {
            return await _playlistRepository.Find(x => x.Username.Equals(username))
                                           .OrderBy(x => x.CreatedDate)
                                           .Take(6).ToListAsync().ConfigureAwait(false);
        }

        [Authorize]
        [HttpGet("/api/get-top-6-last-played-songs")]
        public async Task<List<SongResponseModel>> GetTop6LastPlayedSongs(string username)
        {
            var rs = await _playHistoryRepository.GetTop6LastPlayedSongs(username).ConfigureAwait(false);
            return _mapper.Map<List<SongResponseModel>>(rs);
        }

        [HttpGet("/api/get-trending-songs")]
        public async Task<List<SongResponseModel>> GetTrendingSongs()
        {
            var rs = await _songRepository.Find(x => x.DatePublished >= DateTime.Now.AddYears(-2))
                                       .OrderByDescending(x => x.NumberOfListen)
                                       .Take(6)
                                       .ToListAsync().ConfigureAwait(false);
            return _mapper.Map<List<SongResponseModel>>(rs);
        }

        [Authorize]
        [HttpGet("/api/get-top-6-recommended-songs")]
        public async Task<List<SongResponseModel>> GetTop6RecommendedSongs(string username)
        {
            var songs = await _songRepository.GetAllRecommendedSongs(username).ConfigureAwait(false);
            var rs = songs.Take(6).ToList();
            return _mapper.Map<List<SongResponseModel>>(rs);
        }

        [Authorize]
        [HttpGet("/api/get-all-recommended-songs")]
        public async Task<List<SongResponseModel>> GetAllRecommendedSongs(string username)
        {
            var rs = await _songRepository.GetAllRecommendedSongs(username).ConfigureAwait(false);
            return _mapper.Map<List<SongResponseModel>>(rs);
        }

        [HttpGet("/api/get-recommend-artists")]
        public async Task<List<Artist>> GetRecommendedArtists()
        {
            var rs = await _artistRepository.GetAll().Take(6).ToListAsync().ConfigureAwait(false);
            return rs;
        }

        [HttpGet("/api/get-recommend-albums")]
        public async Task<List<Album>> GetRecommendedAlbums()
        {
            var rs = await _albumRepository.Find(x => x.DatePublished >= DateTime.Now.AddYears(-2))
                .OrderBy(x => x.NumberOfListen)
                .Take(6).ToListAsync().ConfigureAwait(false);
            return rs;
        }

        [HttpGet("/api/test")]
        public void Test()
        {
            InitDatabase.DummyData();
        }


    }
}
