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
        public async Task<IActionResult> GetArtistById(int artistId)
        {
            var rs = await _artistRepository.Find(x => x.Id == artistId).FirstOrDefaultAsync().ConfigureAwait(false);
            return Ok(rs);
        }

        [Authorize]
        [HttpGet("/api/get-follow-artists")]
        public async Task<IActionResult> GetFollowArtists(string username)
        {
            var rs = await _followArtistRepository.GetFollowArtist(username).ConfigureAwait(false);
            return Ok(rs);
        }

        [HttpGet("/api/get-artists-by-keyword")]
        public async Task<IActionResult> GetArtistsByKeyWord(string keyword)
        {
            var rs = await _artistRepository.Find(x => x.Name.ToLower().Contains(keyword.ToLower()))
                .ToListAsync().ConfigureAwait(false);
            return Ok(rs);
        }

        [Authorize]
        [HttpPost("/api/follow-artist")]
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
