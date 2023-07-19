using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLab.Repository;
using MusicLab.Repository.Models;
using MusicLab.Repository.Models.RequestModel;
using MusicLab.Repository.Repositories.Interfaces;

namespace MusicLab.Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private IArtistRepository _artistRepository;
        private IFollowArtistRepository _followArtistRepository;

        public ArtistController(IUnitOfWork unitOfWork)
        {
            _artistRepository = unitOfWork.ArtistRepository;
            _followArtistRepository = unitOfWork.FollowArtistRepository;
        }
  
        [HttpGet("/api/get-artist-by-id")]
        public async Task<Artist> GetArtistById(int artistId)
        {
            return await _artistRepository.Find(x => x.Id == artistId)
                                           .FirstOrDefaultAsync().ConfigureAwait(false);
        }

        [Authorize]
        [HttpGet("/api/get-follow-artists")]
        public async Task<List<Artist>> GetFollowArtists(string username)
        {
            return await _followArtistRepository.GetFollowArtist(username).ConfigureAwait(false);
        }

        [HttpGet("/api/get-artists-by-keyword")]
        public async Task<List<Artist>> GetArtistsByKeyWord(string keyword)
        {
            return await _artistRepository.Find(x => x.Name.ToLower().Contains(keyword.ToLower()))
                .ToListAsync().ConfigureAwait(false);
        }

        [Authorize]
        [HttpGet("/api/follow-artist")]
        public async Task<IActionResult> FollowArtist(FollowArtistRequestModel entity)
        {
            var follow = new FollowArtist
            {
                Username = entity.Username,
                ArtistId = entity.ArtistId,
                FollowDate = DateTime.Now,
            };
            try
            {
                await _followArtistRepository.Add(follow).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
    }
}
