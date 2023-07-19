using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicLab.Backend.Services.Interfaces;
using MusicLab.Repository;
using MusicLab.Repository.Models;
using MusicLab.Repository.Models.RequestModel;
using MusicLab.Repository.Repositories.Interfaces;

namespace MusicLab.Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly ISongRepository _songRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IPlaylistSongRepository _playlistSongRepository;
        private readonly IMapper _mapper;
        private readonly IAWSS3Service _s3Service;

        public PlaylistController(IUnitOfWork unitOfWork, IMapper mapper, IAWSS3Service s3Service)
        {
            _songRepository = unitOfWork.SongRepository;
            _playlistRepository = unitOfWork.PlaylistRepository;
            _playlistSongRepository = unitOfWork.PlaylistSongRepository;
            _mapper = mapper;
            _s3Service = s3Service;
        }

        [Authorize]
        [HttpPost("/api/add-song-to-playlist")]
        public async Task<IActionResult> AddSongToPlaylist(AddSongToPlaylistRequestModel entity)
        {
            var playlistSong = new PlaylistSong
            {
                PlaylistId = entity.PlaylistId,
                SongId = entity.SongId,
                DateAdded = DateTime.Now,
            };
            try
            {
                _playlistSongRepository.Add(playlistSong).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("/api/delete-playlist")]
        public async Task<IActionResult> DeletePlaylist(int playlistId)
        {
            var playlist = await _playlistRepository.GetById(playlistId).ConfigureAwait(false);
            if (playlist == null) return NotFound();
            try
            {
                await _playlistRepository.Delete(playlist).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost("/api/add-playlist")]
        public async Task<IActionResult> AddPlaylist(AddPlaylistRequestModel entity)
        {
            var playlist = new Playlist
            {
                Title = entity.Title,
                Username = entity.Username,
                CreatedDate = DateTime.Now,
            };
            try
            {
                await _playlistRepository.Add(playlist).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
