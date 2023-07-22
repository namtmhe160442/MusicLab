using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicLab.Frontend.Services.Interfaces;
using MusicLab.Repository.Models;
using MusicLab.Repository.Models.ResponseModel;

namespace MusicLab.Frontend.Pages
{
    public class AlbumModel : PageModel
    {
        private readonly IApiCallerService apiCallerService;
        public AlbumResponseModel Album { get; set; }
        public List<SongResponseModel> Songs { get; set; }

        public AlbumModel(IApiCallerService apiCallerService)
        {
            this.apiCallerService = apiCallerService;
        }
        public async Task<IActionResult> OnGet(int albumId)
        {
            Album = await apiCallerService.GetApi<AlbumResponseModel>("https://localhost:7054/api/get-album-by-id?albumId=" + albumId, null);
            Songs = await apiCallerService.GetApi<List<SongResponseModel>>("https://localhost:7054/api/get-songs-by-album?albumId=" + albumId, null);
            return Page();
        }
    }
}
