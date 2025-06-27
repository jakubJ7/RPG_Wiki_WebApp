using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_Wiki_WebApp.Data;

namespace RPG_Wiki_WebApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RPG_Wiki_WebAppContext _context;

        public AdminController(RPG_Wiki_WebAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var totalItems = await _context.Item.CountAsync();
            var totalWeapons = await _context.Weapon.CountAsync();
            var totalArmors = await _context.Armor.CountAsync();
            var totalMiscs = await _context.Misc.CountAsync();

            ViewBag.TotalItems = totalItems;
            ViewBag.TotalWeapons = totalWeapons;
            ViewBag.TotalArmors = totalArmors;
            ViewBag.TotalMiscs = totalMiscs;

            var totalRarities = await _context.ItemRarity.CountAsync();
            var totalArmorTypes = await _context.ArmorType.CountAsync();
            var totalWeaponTypes = await _context.WeaponType.CountAsync();
            var totalMiscTypes = await _context.MiscType.CountAsync();

            ViewBag.TotalRarities = totalRarities;
            ViewBag.TotalArmorTypes = totalArmorTypes;
            ViewBag.TotalWeaponTypes = totalWeaponTypes;
            ViewBag.TotalMiscTypes = totalMiscTypes;

            ViewBag.TotalCharacters = await _context.Character.CountAsync();
            ViewBag.TotalCharacterTypes = await _context.CharacterType.CountAsync();
            ViewBag.PendingCharacters = await _context.Character.CountAsync(c => !c.IsAccepted);

            ViewBag.TotalUsers = await _context.Users.CountAsync();

            return View();
        }

        public async Task<IActionResult> CharacterAccept()
        {
            var pendingCharacters = await _context.Character.Where(c => !c.IsAccepted).ToListAsync();
            return View(pendingCharacters);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveCharacter(int characterId)
        {
            var character = await _context.Character.FindAsync(characterId);
            if (character == null)
            {
                return NotFound();
            }

            character.IsAccepted = true;
            _context.Character.Update(character);
            await _context.SaveChangesAsync();

            return RedirectToAction("CharacterAccept");
        }
    }
}
