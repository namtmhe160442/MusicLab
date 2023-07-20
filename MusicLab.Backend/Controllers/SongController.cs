﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLab.Backend.Services.Interfaces;
using MusicLab.Repository;
using MusicLab.Repository.Models;
using MusicLab.Repository.Models.RequestModel;
using MusicLab.Repository.Models.ResponseModel;
using MusicLab.Repository.Repositories.Interfaces;

namespace MusicLab.Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongRepository _songRepository;
        private readonly IFavouriteRepository _favouriteRepository;
        private readonly IMapper _mapper;
        private readonly IAWSS3Service _s3Service;

        public SongController(IUnitOfWork unitOfWork, IMapper mapper, IAWSS3Service s3Service)
        {
            _songRepository = unitOfWork.SongRepository;
            _favouriteRepository = unitOfWork.FavouriteRepository;
            _mapper = mapper;
            _s3Service = s3Service;
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

        [HttpPost("/api/upload-song")]
        public async Task<IActionResult> UploadSong([FromForm] IFormFile song)
        {
            var success = await _s3Service.UploadFileAsync(song).ConfigureAwait(false);
            if (success) return Ok();
            else return BadRequest();
        }

        [HttpPost("/api/add-song")]
        public async Task<IActionResult> UploadSong(Song song)
        {
            try
            {
                await _songRepository.Add(song).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost("/api/add-favourite")]
        public async Task<IActionResult> AddFavourite(AddFavouriteSongRequestModel entity)
        {
            var favourite = new Favourite
            {
                Username = entity.Username,
                SongId = entity.SongId,
                LikedDate = DateTime.Now,
            };
            try
            {
                await _favouriteRepository.Add(favourite).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("/api/update-number-of-listens")]
        public async Task<IActionResult> UpdateNumberOfListens(int songId)
        {
            var song = await _songRepository.GetById(songId).ConfigureAwait(false);
            try
            {
                song.NumberOfListen += 1;
                await _songRepository.Update(song).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("/api/get-song-url")]
        public async Task<IActionResult> GetSongURL(int songId)
        {
            var song = await _songRepository.GetById(songId).ConfigureAwait(false);
            if (song == null) return BadRequest();
            var url = await _s3Service.GetURLAsync(song.Link).ConfigureAwait(false);
            if (string.IsNullOrEmpty(url)) return BadRequest();
            return Ok(url);
        }
    }
}
