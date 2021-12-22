using Microsoft.AspNetCore.Mvc;

namespace AsteroidTracker.Front.Controllers
{
    public class AsteroidListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
