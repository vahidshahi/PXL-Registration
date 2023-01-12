using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vahid_C_Web_Proj.Data;
using Vahid_C_Web_Proj.Data.DefaultData;
using Vahid_C_Web_Proj.Models.Data;

namespace Vahid_C_Web_Proj.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class LectorController : Controller
    {
        private readonly AppDbContext _context;

        public LectorController(AppDbContext context)
        {
            _context = context;
        }

        //get lector
        public async Task<IActionResult> Index()
        {
            var lectorDbContext = _context.Lector.Include(l => l.Gebruiker);
            return View(await lectorDbContext.ToListAsync());
        }

        //get lector details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lector = await _context.Lector
                .Include(l => l.Gebruiker)
                .FirstOrDefaultAsync(m => m.LectorId == id);
            if (lector == null)
            {
                return NotFound();
            }

            return View(lector);
        }

        //get lector create
        public IActionResult Create()
        {
            ViewData["GebruikerId"] = new SelectList(_context.Gebruikers, "GebruikerId", "GebruikerId");
            return View();
        }

        //post lector create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LectorId,GebruikerId")] Lector lector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GebruikerId"] = new SelectList(_context.Gebruikers, "GebruikerId", "GebruikerId", lector.GebruikerId);
            return View(lector);
        }

        //get lector edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lector = await _context.Lector.FindAsync(id);
            if (lector == null)
            {
                return NotFound();
            }
            ViewData["GebruikerId"] = new SelectList(_context.Gebruikers, "GebruikerId", "GebruikerId", lector.GebruikerId);
            return View(lector);
        }

        //post lector edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LectorId,GebruikerId")] Lector lector)
        {
            if (id != lector.LectorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectorExists(lector.LectorId))
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
            ViewData["GebruikerId"] = new SelectList(_context.Gebruikers, "GebruikerId", "GebruikerId", lector.GebruikerId);
            return View(lector);
        }

        //get lector delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lector = await _context.Lector
                .Include(l => l.Gebruiker)
                .FirstOrDefaultAsync(m => m.LectorId == id);
            if (lector == null)
            {
                return NotFound();
            }

            return View(lector);
        }

        //post lector delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lector = await _context.Lector.FindAsync(id);
            _context.Lector.Remove(lector);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LectorExists(int id)
        {
            return _context.Lector.Any(e => e.LectorId == id);
        }
    }
}
    
