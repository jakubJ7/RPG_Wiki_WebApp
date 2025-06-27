using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPG_Wiki_WebApp.Data;
using RPG_Wiki_WebApp.Models.Item.Misc;

namespace RPG_Wiki_WebApp.Controllers
{
    public class MiscTypesController : Controller
    {
        private readonly RPG_Wiki_WebAppContext _context;

        public MiscTypesController(RPG_Wiki_WebAppContext context)
        {
            _context = context;
        }

        // GET: MiscTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MiscType.ToListAsync());
        }

        // GET: MiscTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miscType = await _context.MiscType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (miscType == null)
            {
                return NotFound();
            }

            return View(miscType);
        }

        // GET: MiscTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MiscTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] MiscType miscType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(miscType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(miscType);
        }

        // GET: MiscTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miscType = await _context.MiscType.FindAsync(id);
            if (miscType == null)
            {
                return NotFound();
            }
            return View(miscType);
        }

        // POST: MiscTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MiscType miscType)
        {
            if (id != miscType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(miscType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiscTypeExists(miscType.Id))
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
            return View(miscType);
        }

        // GET: MiscTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miscType = await _context.MiscType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (miscType == null)
            {
                return NotFound();
            }

            return View(miscType);
        }

        // POST: MiscTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var miscType = await _context.MiscType.FindAsync(id);
            if (miscType != null)
            {
                _context.MiscType.Remove(miscType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MiscTypeExists(int id)
        {
            return _context.MiscType.Any(e => e.Id == id);
        }
    }
}
