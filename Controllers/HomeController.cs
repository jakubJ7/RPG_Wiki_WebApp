using Microsoft.AspNetCore.Mvc;
using RPG_Wiki_WebApp.Data;
using RPG_Wiki_WebApp.Models;
using System.Diagnostics;

namespace RPG_Wiki_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RPG_Wiki_WebAppContext _context;

        public HomeController(ILogger<HomeController> logger, RPG_Wiki_WebAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var weaponTypes = _context.WeaponType.ToList();
            var armorTypes = _context.ArmorType.ToList();
            var miscTypes = _context.MiscType.ToList();
            var characterTypes = _context.CharacterType.ToList();

            ViewBag.WeaponTypes = weaponTypes;
            ViewBag.ArmorTypes = armorTypes;
            ViewBag.MiscTypes = miscTypes;
            ViewBag.CharacterTypes = characterTypes;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
