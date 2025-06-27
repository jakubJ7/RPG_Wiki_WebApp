using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using RPG_Wiki_WebApp.Data;
using RPG_Wiki_WebApp.Models.Item.Weapon;

namespace RPG_Wiki_WebApp.Controllers
{
    public class WeaponsController : Controller
    {
        private readonly RPG_Wiki_WebAppContext _context;

        public WeaponsController(RPG_Wiki_WebAppContext context)
        {
            _context = context;
        }

        // GET: Weapons
        public async Task<IActionResult> Index(string? searchName, int? weaponTypeId, int pageNumber = 1, int pageSize = 10)
        {
            var weapons = _context.Weapon.Include(w => w.ItemRarity).Include(w => w.WeaponType).AsQueryable();

            // Wyszukiwanie po nazwie
            if (!string.IsNullOrEmpty(searchName))
            {
                weapons = weapons.Where(w => w.Name.Contains(searchName));
            }

            if (weaponTypeId.HasValue)
            {
                weapons = weapons.Where(w => w.WeaponTypeId == weaponTypeId.Value);
            }

            // Stronicowanie
            int totalItems = await weapons.CountAsync();
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            weapons = weapons.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            ViewBag.WeaponTypeId = new SelectList(_context.WeaponType, "Id", "Name", weaponTypeId);

            return View(await weapons.ToListAsync());
        }

        // GET: Weapons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapon
                .Include(w => w.ItemRarity)
                .Include(w => w.WeaponType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weapon == null)
            {
                return NotFound();
            }

            return View(weapon);
        }

        // GET: Weapons/Create
        public IActionResult Create()
        {
            ViewData["ItemRarityId"] = new SelectList(_context.ItemRarity, "Id", "Name");
            ViewData["WeaponTypeId"] = new SelectList(_context.Set<WeaponType>(), "Id", "Name");
            return View();
        }

        // POST: Weapons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MinDamage,MaxDamage,WeaponTypeId,Id,ItemCode,Name,Value,ItemRarityId")] Weapon weapon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weapon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemRarityId"] = new SelectList(_context.ItemRarity, "Id", "Id", weapon.ItemRarityId);
            ViewData["WeaponTypeId"] = new SelectList(_context.Set<WeaponType>(), "Id", "Id", weapon.WeaponTypeId);
            return View(weapon);
        }

        // GET: Weapons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapon.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }
            ViewData["ItemRarityId"] = new SelectList(_context.ItemRarity, "Id", "Name", weapon.ItemRarityId);
            ViewData["WeaponTypeId"] = new SelectList(_context.Set<WeaponType>(), "Id", "Name", weapon.WeaponTypeId);
            return View(weapon);
        }

        // POST: Weapons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MinDamage,MaxDamage,WeaponTypeId,Id,ItemCode,Name,Value,ItemRarityId")] Weapon weapon)
        {
            if (id != weapon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weapon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeaponExists(weapon.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemRarityId"] = new SelectList(_context.ItemRarity, "Id", "Id", weapon.ItemRarityId);
            ViewData["WeaponTypeId"] = new SelectList(_context.Set<WeaponType>(), "Id", "Id", weapon.WeaponTypeId);
            return View(weapon);
        }

        // GET: Weapons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapon
                .Include(w => w.ItemRarity)
                .Include(w => w.WeaponType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weapon == null)
            {
                return NotFound();
            }

            return View(weapon);
        }

        // POST: Weapons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weapon = await _context.Weapon.FindAsync(id);
            if (weapon != null)
            {
                _context.Weapon.Remove(weapon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeaponExists(int id)
        {
            return _context.Weapon.Any(e => e.Id == id);
        }
    }
}
