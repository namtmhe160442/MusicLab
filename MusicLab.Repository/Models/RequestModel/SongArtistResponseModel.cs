using MusicLab.Repository.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLab.Repository.Models.RequestModel
{
    public class SongArtistResponseModel
    {
        public int ArtistId { get; set; }
        public int SongId { get; set; }
        public ArtistResponseModel Artist { get; set; }
    }
}
