using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers
{
    public class SpotifyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
