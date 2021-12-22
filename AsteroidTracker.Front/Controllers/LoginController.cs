using AsteroidTracker.Front.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AsteroidTracker.Front.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}