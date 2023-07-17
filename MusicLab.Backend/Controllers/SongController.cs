using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLab.Repository;
using MusicLab.Repository.Models.ResponseModel;
using MusicLab.Repository.Repositories.Interfaces;

namespace MusicLab.Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;

        public SongController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _songRepository = unitOfWork.SongRepository;
            _mapper = mapper;
        }

        [HttpGet("/api/get-song-by-id")]
        public async Task<SongResponseModel> GetSongById(int songId)
        {
            var rs = await _songRepository.Find(x => x.Id == songId)
                                           .FirstOrDefaultAsync().ConfigureAwait(false);
            return _mapper.Map<SongResponseModel>(rs);
        }

        [HttpGet("/api/get-songs-by-keyword")]
        public async Task<List<SongResponseModel>> GetSongsByKeyWord(string keyword)
        {
            var rs = await _songRepository.Find(x => x.Title.ToLower().Contains(keyword.ToLower()))
                .ToListAsync().ConfigureAwait(false);
            return _mapper.Map<List<SongResponseModel>>(rs);
        }
    }
}
