using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicLab.Frontend.Services.Interfaces;
using MusicLab.Repository.Models;
using MusicLab.Repository.Models.RequestModel;
using MusicLab.Repository.Models.ResponseModel;
using Newtonsoft.Json;

namespace MusicLab.Frontend.Pages
{
    public class ArtistModel : PageModel
    {
        private readonly IApiCallerService apiCallerService;
        public ArtistResponseModel Artist;
        public List<SongResponseModel> Songs { get; set; }

        public ArtistModel(IApiCallerService apiCallerService)
        {
            this.apiCallerService = apiCallerService;
        }
        public async Task<IActionResult> OnGet(int artistId)
        {
            Artist = await apiCallerService.GetApi<ArtistResponseModel>("https://localhost:7054/api/get-artist-by-id?artistId=" + artistId, null);
            Songs = await apiCallerService.GetApi<List<SongResponseModel>>("https://localhost:7054/api/get-songs-by-artist?artistId=" + artistId, null);
            return Page();
        }

        public async Task<IActionResult> OnPostFollow(int artistId)
        {
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            var token = HttpContext.Session.GetString("JwtToken");
            FollowArtistRequestModel entity = new FollowArtistRequestModel { Username = user.Username, ArtistId = artistId };
            var rs = await apiCallerService.PostApi("https://localhost:7054/api/follow-artist", entity, token);
            if (rs) return Page();
            else return null;
        }

        public async Task<IActionResult> OnPostUnfollow(int artistId)
        {
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            var token = HttpContext.Session.GetString("JwtToken");
            var rs = await apiCallerService.DeleteApi($"https://localhost:7054/api/unfollow-artist?artistId={artistId}&username={user.Username}", token);
            if (rs) return Page();
            else return null;
        }
    }
}