using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLab.Repository;
using MusicLab.Repository.Models;
using MusicLab.Repository.Models.RequestModel;
using MusicLab.Repository.Models.ResponseModel;
using MusicLab.Repository.Repositories.Interfaces;

namespace MusicLab.Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IFollowArtistRepository _followArtistRepository;
        private readonly IMapper _mapper;

        public ArtistController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _artistRepository = unitOfWork.ArtistRepository;
            _followArtistRepository = unitOfWork.FollowArtistRepository;
            _mapper = mapper;
        }
  
        [HttpGet("/api/get-artist-by-id")]
        public async Task<IActionResult> GetArtistById(int artistId)
        {
            var rs = await _artistRepository.Find(x => x.Id == artistId).FirstOrDefaultAsync().ConfigureAwait(false);
            var rsMap = _mapper.Map<ArtistResponseModel>(rs);
            return Ok(rsMap);
        }

        [Authorize]
        [HttpGet("/api/get-follow-artists")]
        public async Task<IActionResult> GetFollowArtists(string username)
        {
            var rs = await _followArtistRepository.GetFollowArtist(username).ConfigureAwait(false);
            var rsMap = _mapper.Map<List<ArtistResponseModel>>(rs);
            return Ok(rsMap);
        }

        [HttpGet("/api/get-artists-by-keyword")]
        public async Task<IActionResult> GetArtistsByKeyWord(string keyword)
        {
            var rs = await _artistRepository.Find(x => x.Name.ToLower().Contains(keyword.ToLower()))
                .ToListAsync().ConfigureAwait(false);
            var rsMap = _mapper.Map<List<ArtistResponseModel>>(rs);
            return Ok(rsMap);
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
