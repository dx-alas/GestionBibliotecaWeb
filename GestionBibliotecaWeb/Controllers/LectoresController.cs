using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionBibliotecaWeb.Models;
using GestionBibliotecaWeb.Filters;

namespace GestionBibliotecaWeb.Controllers
{
    [SessionAuthorize]
    public class LectoresController : Controller
    {
        private readonly GestionBibliotecaContext _context;

        public LectoresController(GestionBibliotecaContext context)
        {
            _context = context;
        }

        // GET: Lectores
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Lectores.ToListAsync());
        //}

        public async Task<IActionResult> Index(string buscar)
        {
            var lectores = _context.Lectores.AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                lectores = lectores.Where(l =>
                    l.Nombre.Contains(buscar));
            }

            return View(await lectores.ToListAsync());
        }

        // GET: Lectores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectore = await _context.Lectores
                .FirstOrDefaultAsync(m => m.LectorId == id);
            if (lectore == null)
            {
                return NotFound();
            }

            return View(lectore);
        }

        // GET: Lectores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lectores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LectorId,Nombre,Apellido,Documento,Correo,Telefono")] Lectore lectore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lectore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lectore);
        }

        // GET: Lectores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectore = await _context.Lectores.FindAsync(id);
            if (lectore == null)
            {
                return NotFound();
            }
            return View(lectore);
        }

        // POST: Lectores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LectorId,Nombre,Apellido,Documento,Correo,Telefono")] Lectore lectore)
        {
            if (id != lectore.LectorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lectore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectoreExists(lectore.LectorId))
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
            return View(lectore);
        }

        // GET: Lectores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectore = await _context.Lectores
                .FirstOrDefaultAsync(m => m.LectorId == id);
            if (lectore == null)
            {
                return NotFound();
            }

            return View(lectore);
        }

        // POST: Lectores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lectore = await _context.Lectores.FindAsync(id);
            if (lectore != null)
            {
                _context.Lectores.Remove(lectore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LectoreExists(int id)
        {
            return _context.Lectores.Any(e => e.LectorId == id);
        }
    }
}
