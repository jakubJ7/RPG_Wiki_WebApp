using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPG_Wiki_WebApp.Data;
using RPG_Wiki_WebApp.Models.Item.Armor;
using RPG_Wiki_WebApp.Models.Item.Weapon;

namespace RPG_Wiki_WebApp.Controllers
{
    public class ArmorsController : Controller
    {
        private readonly RPG_Wiki_WebAppContext _context;

        public ArmorsController(RPG_Wiki_WebAppContext context)
        {
            _context = context;
        }

        // GET: Armors
        public async Task<IActionResult> Index(string? searchName, int? armorTypeId, int pageNumber = 1, int pageSize = 10 )
        {
            var armors = _context.Armor.Include(a => a.ItemRarity).Include(a => a.ArmorType).AsQueryable();

            // Wyszukiwanie po nazwie
            if (!string.IsNullOrEmpty(searchName))
            {
                armors = armors.Where(a => a.Name.Contains(searchName));
            }

            if (armorTypeId.HasValue)
            {
                armors = armors.Where(a => a.ArmorTypeId == armorTypeId.Value);
            }

            // Stronicowanie
            int totalItems = await armors.CountAsync();
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            armors = armors.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            ViewBag.ArmorTypeId = new SelectList(_context.ArmorType, "Id", "Name", armorTypeId);

            return View(await armors.ToListAsync());
        }

        // GET: Armors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armor = await _context.Armor
                .Include(a => a.ItemRarity)
                .Include(a => a.ArmorType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armor == null)
            {
                return NotFound();
            }

            return View(armor);
        }

        // GET: Armors/Create
        public IActionResult Create()
        {
            ViewData["ItemRarityId"] = new SelectList(_context.ItemRarity, "Id", "Name");
            ViewData["ArmorTypeId"] = new SelectList(_context.ArmorType, "Id", "Name");
            return View();
        }

        // POST: Armors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DamageReduction,ArmorTypeId,Id,ItemCode,Name,Value,ItemRarityId")] Armor armor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(armor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemRarityId"] = new SelectList(_context.ItemRarity, "Id", "Id", armor.ItemRarityId);
            ViewData["ArmorTypeId"] = new SelectList(_context.ArmorType, "Id", "Id", armor.ArmorTypeId);
            return View(armor);
        }

        // GET: Armors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armor = await _context.Armor.FindAsync(id);
            if (armor == null)
            {
                return NotFound();
            }
            ViewData["ItemRarityId"] = new SelectList(_context.ItemRarity, "Id", "Name", armor.ItemRarityId);
            ViewData["ArmorTypeId"] = new SelectList(_context.ArmorType, "Id", "Name", armor.ArmorTypeId);
            return View(armor);
        }

        // POST: Armors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DamageReduction,ArmorTypeId,Id,ItemCode,Name,Value,ItemRarityId")] Armor armor)
        {
            if (id != armor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(armor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArmorExists(armor.Id))
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
            ViewData["ItemRarityId"] = new SelectList(_context.ItemRarity, "Id", "Id", armor.ItemRarityId);
            ViewData["ArmorTypeId"] = new SelectList(_context.ArmorType, "Id", "Id", armor.ArmorTypeId);
            return View(armor);
        }

        // GET: Armors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armor = await _context.Armor
                .Include(a => a.ItemRarity)
                .Include(a => a.ArmorType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armor == null)
            {
                return NotFound();
            }

            return View(armor);
        }

        // POST: Armors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var armor = await _context.Armor.FindAsync(id);
            if (armor != null)
            {
                _context.Armor.Remove(armor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArmorExists(int id)
        {
            return _context.Armor.Any(e => e.Id == id);
        }
    }
}
