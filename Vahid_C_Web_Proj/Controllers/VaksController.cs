using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vahid_C_Web_Proj.Data;
using Vahid_C_Web_Proj.Data.DefaultData;
using Vahid_C_Web_Proj.Models.Data;

namespace Vahid_C_Web_Proj.Controllers
{
    [Authorize]
    public class VaksController : Controller
    {
        private readonly AppDbContext _context;

        public VaksController(AppDbContext context)
        {
            _context = context;
        }

        //Get:Vaks
        [Authorize(Roles = Roles.Admin_Student)]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(Roles.Lector))
            {
                var lector = _context.Lector.Include(x => x.Gebruiker).FirstOrDefault(x => x.Gebruiker.Email.Equals(User.Identity.Name));
                var vakken = _context.VakLectors.Include(x => x.Lector).Include(x => x.Vak)
                    .Where(x => x.Lector.LectorId == lector.LectorId)
                    .Select(x => x.Vak);
                return View(await vakken.ToListAsync());
            }
            var appDbContext = _context.Vaks.Include(v => v.Handboek);
            return View(await appDbContext.ToListAsync());
        }

        //Get:Vaks/Details/5
        [Authorize(Roles = Roles.Admin_Student)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vak = await _context.Vaks
                .Include(v => v.Handboek)
                .FirstOrDefaultAsync(m => m.VakId == id);
            if (vak == null)
            {
                return NotFound();
            }

            return View(vak);
        }

        //Get:Vaks/Create
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Create()
        {
            ViewData["HandboekId"] = new SelectList(_context.Handboeks, "HandboekId", "HandboekId");
            return View();
        }

        //Post:Vaks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create([Bind("VakId,Naam,HandboekId")] Vak vak)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HandboekId"] = new SelectList(_context.Handboeks, "HandboekId", "HandboekId", vak.HandboekID);
            return View(vak);
        }

        //Get:Vaks/Edit/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vak = await _context.Vaks.FindAsync(id);
            if (vak == null)
            {
                return NotFound();
            }
            ViewData["HandboekId"] = new SelectList(_context.Handboeks, "HandboekId", "HandboekId", vak.HandboekID);
            return View(vak);
        }

        //Post:Vaks/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int id, [Bind("VakId,Naam,HandboekId")] Vak vak)
        {
            if (id != vak.VakId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VakExists(vak.VakId))
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
            ViewData["HandboekId"] = new SelectList(_context.Handboeks, "HandboekId", "HandboekId", vak.HandboekID);
            return View(vak);
        }

        //Get:Vaks/Delete/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vak = await _context.Vaks
                .Include(v => v.Handboek)
                .FirstOrDefaultAsync(m => m.VakId == id);
            if (vak == null)
            {
                return NotFound();
            }

            return View(vak);
        }

        //Post:Vaks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vak = await _context.Vaks.FindAsync(id);
            _context.Vaks.Remove(vak);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VakExists(int id)
        {
            return _context.Vaks.Any(e => e.VakId == id);
        }
            
        

    }
}
