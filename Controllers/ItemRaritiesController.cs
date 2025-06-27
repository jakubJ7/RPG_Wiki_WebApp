using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPG_Wiki_WebApp.Data;
using RPG_Wiki_WebApp.Models.Item;

namespace RPG_Wiki_WebApp.Controllers
{
    public class ItemRaritiesController : Controller
    {
        private readonly RPG_Wiki_WebAppContext _context;

        public ItemRaritiesController(RPG_Wiki_WebAppContext context)
        {
            _context = context;
        }

        // GET: ItemRarities
        public async Task<IActionResult> Index()
        {
            return View(await _context.ItemRarity.ToListAsync());
        }

        // GET: ItemRarities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemRarity = await _context.ItemRarity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemRarity == null)
            {
                return NotFound();
            }

            return View(itemRarity);
        }

        // GET: ItemRarities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemRarities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ItemRarity itemRarity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemRarity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemRarity);
        }

        // GET: ItemRarities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemRarity = await _context.ItemRarity.FindAsync(id);
            if (itemRarity == null)
            {
                return NotFound();
            }
            return View(itemRarity);
        }

        // POST: ItemRarities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ItemRarity itemRarity)
        {
            if (id != itemRarity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemRarity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemRarityExists(itemRarity.Id))
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
            return View(itemRarity);
        }

        // GET: ItemRarities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemRarity = await _context.ItemRarity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemRarity == null)
            {
                return NotFound();
            }

            return View(itemRarity);
        }

        // POST: ItemRarities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemRarity = await _context.ItemRarity.FindAsync(id);
            if (itemRarity != null)
            {
                _context.ItemRarity.Remove(itemRarity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemRarityExists(int id)
        {
            return _context.ItemRarity.Any(e => e.Id == id);
        }
    }
}
