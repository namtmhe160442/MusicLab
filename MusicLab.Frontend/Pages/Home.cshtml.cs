using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicLab.Frontend.Services.Interfaces;
using MusicLab.Repository.Models;
using Newtonsoft.Json;

namespace MusicLab.Frontend.Pages
{
    public class HomeModel : PageModel
    {
        private readonly IApiCallerService apiCallerService;

        public HomeModel(IApiCallerService apiCallerService)
        {
            this.apiCallerService = apiCallerService;
        }
        public async void OnGet()
        {
            var userJson = HttpContext.Session.GetString("User");
            if (!string.IsNullOrEmpty(userJson)) {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                var playlist = await apiCallerService.GetApi<List<Playlist>>("https://localhost:7054/api/get-all-playlists?username=" + user.Username,
                HttpContext.Session.GetString("JwtToken"));
            }
        }
    }
}
