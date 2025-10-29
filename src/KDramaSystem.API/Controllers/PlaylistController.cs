using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers
{
    public class PlaylistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
