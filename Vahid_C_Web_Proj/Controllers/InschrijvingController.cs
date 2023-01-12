using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vahid_C_Web_Proj.Data;
using Vahid_C_Web_Proj.Models.Data;
using Vahid_C_Web_Proj.Models.ViewModel;

namespace Vahid_C_Web_Proj.Controllers
{
    public class InschrijvingController : Controller
    {
        //Dependency Injection
        private AppDbContext _context;

        public InschrijvingController(AppDbContext context)
        {
            _context = context;
        }

     
        public async Task<IActionResult> Index()
        {
            var CursusContext = _context.Inschrijvingen.Include(i => i.AcademieJaar)
                .Include(i => i.Student).ThenInclude(x => x.Gebruiker)
                .Include(i => i.VakLector).ThenInclude(x => x.Vak)
                .Include(i => i.VakLector).ThenInclude(x => x.Lector).ThenInclude(x => x.Gebruiker);
            return View(await CursusContext.ToListAsync());
        }

        // GET: Inschrijvings/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijvingen
                .Include(i => i.AcademieJaar)
                .Include(i => i.Student).ThenInclude(x => x.Gebruiker)
                .Include(i => i.VakLector).ThenInclude(x => x.Vak)
                .Include(i => i.VakLector).ThenInclude(x => x.Lector).ThenInclude(x => x.Gebruiker)
                .FirstOrDefaultAsync(m => m.InschrijvingId == id);
            if (inschrijving == null)
            {
                return NotFound();
            }

            return View(inschrijving);
        }

        // GET: Inschrijvings/Create

        public IActionResult Create()
        {
            ViewData["AcademieJaarId"] = new SelectList(_context.AcademieJaars, "AcademieJaarId", "AcademieJaarId");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            ViewData["VakLectorId"] = new SelectList(_context.VakLectors, "VakLectorId", "VakLectorId");
            return View();
        }

        // POST: Inschrijvings/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InschrijvingId,AcademieJaarId,StudentId,VakLectorId")] Inschrijving inschrijving)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inschrijving);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademieJaarId"] = new SelectList(_context.AcademieJaars, "AcademieJaarId", "AcademieJaarId", inschrijving.AcademieJaarId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", inschrijving.StudentId);
            ViewData["VakLectorId"] = new SelectList(_context.VakLectors, "VakLectorId", "VakLectorId", inschrijving.VakLectorId);
            return View(inschrijving);
        }

        // GET: Inschrijvings/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijvingen.FindAsync(id);
            if (inschrijving == null)
            {
                return NotFound();
            }
            ViewData["AcademieJaarId"] = new SelectList(_context.AcademieJaars, "AcademieJaarId", "AcademieJaarId", inschrijving.AcademieJaarId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", inschrijving.StudentId);
            ViewData["VakLectorId"] = new SelectList(_context.VakLectors, "VakLectorId", "VakLectorId", inschrijving.VakLectorId);
            return View(inschrijving);
        }

        // POST: Inschrijvings/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InschrijvingId,AcademieJaarId,StudentId,VakLectorId")] Inschrijving inschrijving)
        {
            if (id != inschrijving.InschrijvingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inschrijving);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InschrijvingExists(inschrijving.InschrijvingId))
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
            ViewData["AcademieJaarId"] = new SelectList(_context.AcademieJaars, "AcademieJaarId", "AcademieJaarId", inschrijving.AcademieJaarId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", inschrijving.StudentId);
            ViewData["VakLectorId"] = new SelectList(_context.VakLectors, "VakLectorId", "VakLectorId", inschrijving.VakLectorId);
            return View(inschrijving);
        }

        // GET: Inschrijvings/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijvingen
                .Include(i => i.AcademieJaar)
                .Include(i => i.Student).ThenInclude(x => x.Gebruiker)
                .Include(i => i.VakLector).ThenInclude(x => x.Vak)
                .Include(i => i.VakLector).ThenInclude(x => x.Lector).ThenInclude(x => x.Gebruiker)
                .FirstOrDefaultAsync(m => m.InschrijvingId == id);
            if (inschrijving == null)
            {
                return NotFound();
            }

            return View(inschrijving);
        }

        // POST: Inschrijvings/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inschrijving = await _context.Inschrijvingen.FindAsync(id);
            _context.Inschrijvingen.Remove(inschrijving);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InschrijvingExists(int id)
        {
            return _context.Inschrijvingen.Any(e => e.InschrijvingId == id);
        }
        
        
        


    }
}