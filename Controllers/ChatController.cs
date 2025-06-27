using Microsoft.AspNetCore.Mvc;

namespace RPG_Wiki_WebApp.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
