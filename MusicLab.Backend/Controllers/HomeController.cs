using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicLab.Backend.Services.Interfaces;

namespace MusicLab.Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IAWSS3Service AWSS3Service { get; set; }

        public HomeController(IAWSS3Service aWSS3Service)
        {
            AWSS3Service = aWSS3Service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Home()
        {
            return null;
        }
    }
}
