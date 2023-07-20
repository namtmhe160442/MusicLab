using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc;
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

        public IList<Playlist> listPlaylists { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var userJson = HttpContext.Session.GetString("User");
            if (!string.IsNullOrEmpty(userJson)) {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                var Token = HttpContext.Session.GetString("JwtToken");
                listPlaylists = await apiCallerService.GetApi<List<Playlist>>("https://localhost:7054/api/get-all-playlists?username=" + user.Username, Token);
                return Page();
            }
            return Page();
        }
    }
}
