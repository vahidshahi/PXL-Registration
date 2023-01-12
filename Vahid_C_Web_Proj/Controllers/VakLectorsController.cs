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
    public class VakLectorsController : Controller
    {
        //Dependecy Injection
        private readonly AppDbContext _context;

        public VakLectorsController(AppDbContext context)
        {
            _context = context;
        }
        //Get : VakLectors
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.VakLectors.Include(v => v.Lector).ThenInclude(v => v.Gebruiker)
                .Include(v => v.Vak).ThenInclude(v => v.Handboek);
            return View(await appDbContext.ToListAsync());
        }

        //Get VakLectors / Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vakLector = await _context.VakLectors
                .Include(v => v.Lector).ThenInclude(v => v.Gebruiker)
                .Include(v => v.Vak).ThenInclude(v => v.Handboek)
                .FirstOrDefaultAsync(m => m.VakLectorId == id);
            if (vakLector == null)
            {
                return NotFound();
            }

            return View(vakLector);
        }

        //Get VakLectors / Create

        public IActionResult Create()
        {
            ViewData["LectorId"] = new SelectList(_context.Lector.Include(x => x.Gebruiker), "LectorId", "Gebruiker.Naam");
            ViewData["VakId"] = new SelectList(_context.Vaks.Include(x => x.Handboek), "VakId", "Handboek.Naam");
            return View();
        }

        //Post VakLectors / Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VakLectorId,VakId,LectorId")] VakLector vakLector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vakLector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LectorId"] = new SelectList(_context.Lector.Include(x => x.Gebruiker), "LectorId", "Gebruiker.Naam", vakLector.LectorId);
            ViewData["VakId"] = new SelectList(_context.Vaks.Include(x => x.Handboek), "VakId", "Handboek.Naam", vakLector.VakId);
            return View(vakLector);
        }

        //Get VakLectors / Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vakLector = await _context.VakLectors.FindAsync(id);
            if (vakLector == null)
            {
                return NotFound();
            }
            ViewData["LectorId"] = new SelectList(_context.Lector.Include(x => x.Gebruiker), "LectorId", "Gebruiker.Naam", vakLector.LectorId);
            ViewData["VakId"] = new SelectList(_context.Vaks.Include(x => x.Handboek), "VakId", "Handboek.Naam", vakLector.VakId);
            return View(vakLector);
        }

        //Post VakLectors / Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VakLectorId,VakId,LectorId")] VakLector vakLector)
        {
            if (id != vakLector.VakLectorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vakLector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VakLectorExists(vakLector.VakLectorId))
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
            ViewData["LectorId"] = new SelectList(_context.Lector.Include(x => x.Gebruiker), "LectorId", "Gebruiker.Naam", vakLector.LectorId);
            ViewData["VakId"] = new SelectList(_context.Vaks.Include(x => x.Handboek), "VakId", "Handboek.Naam", vakLector.VakId);
            return View(vakLector);
        }

        //Get VakLectors / Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vakLector = await _context.VakLectors
                .Include(v => v.Lector).ThenInclude(v => v.Gebruiker)
                .Include(v => v.Vak).ThenInclude(v => v.Handboek)
                .FirstOrDefaultAsync(m => m.VakLectorId == id);
            if (vakLector == null)
            {
                return NotFound();
            }

            return View(vakLector);
        }

        //Post VakLectors / Delete/5


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vakLector = await _context.VakLectors.FindAsync(id);
            _context.VakLectors.Remove(vakLector);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VakLectorExists(int id)
        {
            return _context.VakLectors.Any(e => e.VakLectorId == id);
        }





    }
}
