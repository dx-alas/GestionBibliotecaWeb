using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionBibliotecaWeb.Models;
using GestionBibliotecaWeb.Services;
using GestionBibliotecaWeb.Filters;

namespace GestionBibliotecaWeb.Controllers
{
    [SessionAuthorize]
    public class LibrosController : Controller
    {
        private readonly GestionBibliotecaContext _context;
        private readonly CloudinaryService _cloudinary;

        public LibrosController(GestionBibliotecaContext context, CloudinaryService cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
        }

        // GET: Libros
        //public async Task<IActionResult> Index()
        //{
        //    var gestionBibliotecaContext = _context.Libros.Include(l => l.Autor);
        //    return View(await gestionBibliotecaContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(string buscar)
        {
            var libros = _context.Libros
                .Include(l => l.Autor)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                libros = libros.Where(l =>
                    l.Titulo.Contains(buscar) ||
                    l.Isbn.Contains(buscar) ||
                    l.Autor.Nombre.Contains(buscar));
            }

            return View(await libros.ToListAsync());
        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(m => m.LibroId == id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(
                _context.Autores.OrderBy(a => a.AutorId),
                "AutorId",
                "NombreCompleto"
            );

            return View();
        }

        // POST: Libros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libro libro, IFormFile imagenFile)
        {
            if (ModelState.IsValid)
            {
                if (imagenFile != null && imagenFile.Length > 0)
                {
                    var url = await _cloudinary.SubirImagen(imagenFile);
                    libro.ImagenUrl = url;
                }

                _context.Add(libro);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["AutorId"] = new SelectList(
                _context.Autores.OrderBy(a => a.AutorId),
                "AutorId",
                "NombreCompleto",
                libro.AutorId
            );

            return View(libro);
        }

        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            ViewData["AutorId"] = new SelectList(
                _context.Autores.OrderBy(a => a.AutorId),
                "AutorId",
                "NombreCompleto",
                libro.AutorId
            );

            return View(libro);
        }

        // POST: Libros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Libro libro)
        {
            if (id != libro.LibroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var imagenFile = Request.Form.Files.GetFile("imagenFile");

                    if (imagenFile != null && imagenFile.Length > 0)
                    {
                        var url = await _cloudinary.SubirImagen(imagenFile);
                        libro.ImagenUrl = url;
                    }

                    _context.Update(libro);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error: " + ex.Message);
                }
            }

            ViewData["AutorId"] = new SelectList(
                _context.Autores.OrderBy(a => a.AutorId),
                "AutorId",
                "NombreCompleto",
                libro.AutorId
            );

            return View(libro);
        }

        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(m => m.LibroId == id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro != null)
            {
                _context.Libros.Remove(libro);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.LibroId == id);
        }
    }
}