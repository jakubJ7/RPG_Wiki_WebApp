using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPG_Wiki_WebApp.Data;
using RPG_Wiki_WebApp.Models.Item.Weapon;

namespace RPG_Wiki_WebApp.Controllers
{
    public class WeaponTypesController : Controller
    {
        private readonly RPG_Wiki_WebAppContext _context;

        public WeaponTypesController(RPG_Wiki_WebAppContext context)
        {
            _context = context;
        }

        // GET: WeaponTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.WeaponType.ToListAsync());
        }

        // GET: WeaponTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weaponType = await _context.WeaponType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weaponType == null)
            {
                return NotFound();
            }

            return View(weaponType);
        }

        // GET: WeaponTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeaponTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] WeaponType weaponType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weaponType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weaponType);
        }

        // GET: WeaponTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weaponType = await _context.WeaponType.FindAsync(id);
            if (weaponType == null)
            {
                return NotFound();
            }
            return View(weaponType);
        }

        // POST: WeaponTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] WeaponType weaponType)
        {
            if (id != weaponType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weaponType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeaponTypeExists(weaponType.Id))
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
            return View(weaponType);
        }

        // GET: WeaponTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weaponType = await _context.WeaponType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weaponType == null)
            {
                return NotFound();
            }

            return View(weaponType);
        }

        // POST: WeaponTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weaponType = await _context.WeaponType.FindAsync(id);
            if (weaponType != null)
            {
                _context.WeaponType.Remove(weaponType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeaponTypeExists(int id)
        {
            return _context.WeaponType.Any(e => e.Id == id);
        }
    }
}
