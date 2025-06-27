using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RPG_Wiki_WebApp.Models;
using RPG_Wiki_WebApp.ViewModels;
using System.Threading.Tasks;
using RPG_Wiki_WebApp.Models; // dla ViewModels

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        Console.WriteLine($"Rejestracja użytkownika: {model.Username}, {model.Email}");
        if (!ModelState.IsValid) return View(model);

        var user = new ApplicationUser { UserName = model.Username, Email = model.Email, Nickname = model.Nickname };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            Console.WriteLine("Rejestracja zakończona sukcesem!");
            return RedirectToAction("Login");
        }

        foreach (var error in result.Errors)
        {
            Console.WriteLine($"Błąd: {error.Description}");
            ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
        return result.Succeeded ? RedirectToAction("Index", "Home") : View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}
