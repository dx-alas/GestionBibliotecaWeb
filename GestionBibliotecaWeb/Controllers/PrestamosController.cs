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
    // Validación de sesión activa
    [SessionAuthorize]
    public class PrestamosController : Controller
    {
        private readonly GestionBibliotecaContext _context;

        public PrestamosController(GestionBibliotecaContext context)
        {
            _context = context;
        }

        // GET: Prestamoes
        public async Task<IActionResult> Index()
        {
            // Incluye relaciones de lector, libro y usuario
            var gestionBibliotecaContext = _context.Prestamos.Include(p => p.Lector).Include(p => p.Libro).Include(p => p.Usuario);
            return View(await gestionBibliotecaContext.ToListAsync());
        }

        // GET: Prestamoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Buscar préstamo por id
            var prestamo = await _context.Prestamos
                .Include(p => p.Lector)
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.PrestamoId == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // GET: Prestamoes/Create
        public IActionResult Create()
        {
            ViewData["LectorId"] = new SelectList
                (_context.Lectores.OrderBy(a => a.LectorId),
                "LectorId", 
                "NombreCompleto"
            );
            ViewData["LibroId"] = new SelectList(
                _context.Libros.OrderBy(a => a.LibroId), 
                "LibroId",
                "NombreCompleto"
            );
            ViewData["UsuarioId"] = new SelectList(
                _context.Usuarios.OrderBy(a => a.UsuarioId),
                "UsuarioId",
                "NombreCompleto"
            );

            return View();
        }

        // POST: Prestamoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrestamoId,LibroId,LectorId,FechaPrestamo,FechaDevolucion,Observacion,UsuarioId")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recargar listas si hay error
            ViewData["LectorId"] = new SelectList
                (_context.Lectores.OrderBy(a => a.LectorId),
                "LectorId",
                "NombreCompleto"
            );
            ViewData["LibroId"] = new SelectList(
                _context.Libros.OrderBy(a => a.LibroId),
                "LibroId",
                "NombreCompleto"
            );
            ViewData["UsuarioId"] = new SelectList(
                _context.Usuarios.OrderBy(a => a.UsuarioId),
                "UsuarioId",
                "NombreCompleto"
            );

            return View(prestamo);
        }

        // GET: Prestamoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Buscar préstamo
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }

            ViewData["LectorId"] = new SelectList
                (_context.Lectores.OrderBy(a => a.LectorId),
                "LectorId",
                "NombreCompleto"
            );
            ViewData["LibroId"] = new SelectList(
                _context.Libros.OrderBy(a => a.LibroId),
                "LibroId",
                "NombreCompleto"
            );
            ViewData["UsuarioId"] = new SelectList(
                _context.Usuarios.OrderBy(a => a.UsuarioId),
                "UsuarioId",
                "NombreCompleto"
            );

            return View(prestamo);
        }

        // POST: Prestamoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrestamoId,LibroId,LectorId,FechaPrestamo,FechaDevolucion,Observacion,UsuarioId")] Prestamo prestamo)
        {
            if (id != prestamo.PrestamoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.PrestamoId))
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

            ViewData["LectorId"] = new SelectList
                (_context.Lectores.OrderBy(a => a.LectorId),
                "LectorId",
                "NombreCompleto"
            );
            ViewData["LibroId"] = new SelectList(
                _context.Libros.OrderBy(a => a.LibroId),
                "LibroId",
                "NombreCompleto"
            );
            ViewData["UsuarioId"] = new SelectList(
                _context.Usuarios.OrderBy(a => a.UsuarioId),
                "UsuarioId",
                "NombreCompleto"
            );

            return View(prestamo);
        }

        // GET: Prestamoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Lector)
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.PrestamoId == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                _context.Prestamos.Remove(prestamo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamos.Any(e => e.PrestamoId == id);
        }
    }
}
