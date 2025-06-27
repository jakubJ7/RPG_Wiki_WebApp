using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPG_Wiki_WebApp.Data;
using RPG_Wiki_WebApp.Models.Character;

namespace RPG_Wiki_WebApp.Controllers
{
    public class CharacterTypesController : Controller
    {
        private readonly RPG_Wiki_WebAppContext _context;

        public CharacterTypesController(RPG_Wiki_WebAppContext context)
        {
            _context = context;
        }

        // GET: CharacterTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CharacterType.ToListAsync());
        }

        // GET: CharacterTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characterType = await _context.CharacterType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characterType == null)
            {
                return NotFound();
            }

            return View(characterType);
        }

        // GET: CharacterTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CharacterTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CharacterType characterType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(characterType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(characterType);
        }

        // GET: CharacterTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characterType = await _context.CharacterType.FindAsync(id);
            if (characterType == null)
            {
                return NotFound();
            }
            return View(characterType);
        }

        // POST: CharacterTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CharacterType characterType)
        {
            if (id != characterType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(characterType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterTypeExists(characterType.Id))
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
            return View(characterType);
        }

        // GET: CharacterTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characterType = await _context.CharacterType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characterType == null)
            {
                return NotFound();
            }

            return View(characterType);
        }

        // POST: CharacterTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var characterType = await _context.CharacterType.FindAsync(id);
            if (characterType != null)
            {
                _context.CharacterType.Remove(characterType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterTypeExists(int id)
        {
            return _context.CharacterType.Any(e => e.Id == id);
        }
    }
}
