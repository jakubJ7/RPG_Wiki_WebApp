using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPG_Wiki_WebApp.Data;
using RPG_Wiki_WebApp.Models.Item.Armor;
using RPG_Wiki_WebApp.Models.Item.Misc;

namespace RPG_Wiki_WebApp.Controllers
{
    public class MiscsController : Controller
    {
        private readonly RPG_Wiki_WebAppContext _context;

        public MiscsController(RPG_Wiki_WebAppContext context)
        {
            _context = context;
        }

        // GET: Miscs
        public async Task<IActionResult> Index(string? searchName, int? miscTypeId, int pageNumber = 1, int pageSize = 10)
        {
            var miscs = _context.Misc.Include(m => m.ItemRarity).Include(m => m.MiscType).AsQueryable();

            // Wyszukiwanie po nazwie
            if (!string.IsNullOrEmpty(searchName))
            {
                miscs = miscs.Where(a => a.Name.Contains(searchName));
            }

            if (miscTypeId.HasValue)
            {
                miscs = miscs.Where(a => a.MiscTypeId == miscTypeId.Value);
            }

            // Stronicowanie
            int totalItems = await miscs.CountAsync();
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            miscs = miscs.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            ViewBag.MiscTypeId = new SelectList(_context.MiscType, "Id", "Name", miscTypeId);

            return View(await miscs.ToListAsync());
        }

        // GET: Miscs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var misc = await _context.Misc
                .Include(m => m.ItemRarity)
                .Include(m => m.MiscType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (misc == null)
            {
                return NotFound();
            }

            return View(misc);
        }

        // GET: Miscs/Create
        public IActionResult Create()
        {
            ViewData["ItemRarityId"] = new SelectList(_context.ItemRarity, "Id", "Name");
            ViewData["MiscTypeId"] = new SelectList(_context.Set<MiscType>(), "Id", "Name");
            return View();
        }

        // POST: Miscs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MiscTypeId,Id,ItemCode,Name,Value,ItemRarityId")] Misc misc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(misc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemRarityId"] = new SelectList(_context.ItemRarity, "Id", "Id", misc.ItemRarityId);
            ViewData["MiscTypeId"] = new SelectList(_context.Set<MiscType>(), "Id", "Id", misc.MiscTypeId);
            return View(misc);
        }

        // GET: Miscs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var misc = await _context.Misc.FindAsync(id);
            if (misc == null)
            {
                return NotFound();
            }
            ViewData["ItemRarityId"] = new SelectList(_context.ItemRarity, "Id", "Name", misc.ItemRarityId);
            ViewData["MiscTypeId"] = new SelectList(_context.Set<MiscType>(), "Id", "Name", misc.MiscTypeId);
            return View(misc);
        }

        // POST: Miscs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MiscTypeId,Id,ItemCode,Name,Value,ItemRarityId")] Misc misc)
        {
            if (id != misc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(misc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiscExists(misc.Id))
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
            ViewData["ItemRarityId"] = new SelectList(_context.ItemRarity, "Id", "Id", misc.ItemRarityId);
            ViewData["MiscTypeId"] = new SelectList(_context.Set<MiscType>(), "Id", "Id", misc.MiscTypeId);
            return View(misc);
        }

        // GET: Miscs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var misc = await _context.Misc
                .Include(m => m.ItemRarity)
                .Include(m => m.MiscType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (misc == null)
            {
                return NotFound();
            }

            return View(misc);
        }

        // POST: Miscs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var misc = await _context.Misc.FindAsync(id);
            if (misc != null)
            {
                _context.Misc.Remove(misc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MiscExists(int id)
        {
            return _context.Misc.Any(e => e.Id == id);
        }
    }
}
