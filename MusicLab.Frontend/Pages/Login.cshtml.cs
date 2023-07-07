using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
namespace MusicLab.Frontend.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LoginModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> OnPostAsync(string username, string password)
        {
            var loginUrl = "https://localhost:7054/api/login";

            var loginData = new
            {
                username,
                password
            };

            var response = await _httpClient.PostAsJsonAsync(loginUrl, loginData);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                 var token = JsonConvert.DeserializeObject<TokenResponse>(responseData);

                HttpContext.Session.SetString("JwtToken", token.Token);

                return RedirectToPage("/Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return Page();
            }
        }
    }
    public class TokenResponse
    {
        public string Token { get; set; }
    }
}
