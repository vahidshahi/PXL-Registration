using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Vahid_C_Web_Proj.CustomValidation;
using Vahid_C_Web_Proj.Data;
using Vahid_C_Web_Proj.Data.DefaultData;
using Vahid_C_Web_Proj.Models.Data;

namespace Vahid_C_Web_Proj.Controllers
{
    public class HandboekController : Controller
    {
        //Dependency Injection
        private readonly AppDbContext _context;

        public HandboekController(AppDbContext context)
        {
            _context = context;
        }


        //Get : Handboeks
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(Roles.Lector))
            {
                var l = _context.Lector.Include(x => x.Gebruiker).FirstOrDefault(x => x.Gebruiker.Email.Equals(User.Identity.Name));
                var h = _context.VakLectors.Include(x => x.Lector)
                    .Include(x => x.Vak).ThenInclude(x => x.Handboek)
                    .Where(x => x.Lector.LectorId == l.LectorId)
                    .Select(x => x.Vak.Handboek);
                return View(await h.ToListAsync());
            }
            return View(await _context.Handboeks.ToListAsync());
        }


        //Get Handboek / Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var handboek = await _context.Handboeks
                .FirstOrDefaultAsync(m => m.HandboekId == id);
            if (handboek == null)
            {
                return NotFound();
            }

            return View(handboek);
        }

        //Get Handboek / Create
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Create()
        {
            return View();
        }


        //Post Handboek / Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create([Bind("HandboekId,Titel,Kostprijs,UitgifteDatum,Afbeelding")] Handboek handboek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(handboek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(handboek);
        }

        //Get: Handboek / Edit
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var handboek = await _context.Handboeks.FindAsync(id);
            if (handboek == null)
            {
                return NotFound();
            }
            return View(handboek);
        }

        //Post: Handboek / Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int id, [Bind("HandboekId,Titel,Kostprijs,UitgifteDatum,Afbeelding")] Handboek handboek)
        {
            if (id != handboek.HandboekId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(handboek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HandboekExists(handboek.HandboekId))
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
            return View(handboek);
        }

        //Get: Handboek / Delete
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var handboek = await _context.Handboeks
                .FirstOrDefaultAsync(m => m.HandboekId == id);
            if (handboek == null)
            {
                return NotFound();
            }

            return View(handboek);
        }

        //Post: Handboek / Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var handboek = await _context.Handboeks.FindAsync(id);
            _context.Handboeks.Remove(handboek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HandboekExists(int id)
        {
            return _context.Handboeks.Any(e => e.HandboekId == id);
        }
    }
}
    
