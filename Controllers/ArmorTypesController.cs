using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPG_Wiki_WebApp.Data;
using RPG_Wiki_WebApp.Models.Item.Armor;

namespace RPG_Wiki_WebApp.Controllers
{
    public class ArmorTypesController : Controller
    {
        private readonly RPG_Wiki_WebAppContext _context;

        public ArmorTypesController(RPG_Wiki_WebAppContext context)
        {
            _context = context;
        }

        // GET: ArmorTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArmorType.ToListAsync());
        }

        // GET: ArmorTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armorType = await _context.ArmorType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armorType == null)
            {
                return NotFound();
            }

            return View(armorType);
        }

        // GET: ArmorTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArmorTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ArmorType armorType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(armorType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(armorType);
        }

        // GET: ArmorTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armorType = await _context.ArmorType.FindAsync(id);
            if (armorType == null)
            {
                return NotFound();
            }
            return View(armorType);
        }

        // POST: ArmorTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ArmorType armorType)
        {
            if (id != armorType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(armorType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArmorTypeExists(armorType.Id))
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
            return View(armorType);
        }

        // GET: ArmorTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armorType = await _context.ArmorType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armorType == null)
            {
                return NotFound();
            }

            return View(armorType);
        }

        // POST: ArmorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var armorType = await _context.ArmorType.FindAsync(id);
            if (armorType != null)
            {
                _context.ArmorType.Remove(armorType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArmorTypeExists(int id)
        {
            return _context.ArmorType.Any(e => e.Id == id);
        }
    }
}
