using AutoMapper;
using MusicLab.Repository.Models;
using MusicLab.Repository.Models.ResponseModel;

namespace MusicLab.Backend.Automapper
{
    public class DomainToView : Profile
    {
        public DomainToView()
        {
            CreateMap<Song, SongResponseModel>();
        }
    }
}
