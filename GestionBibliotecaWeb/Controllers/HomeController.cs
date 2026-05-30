using GestionBibliotecaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionBibliotecaWeb.Filters;

namespace GestionBibliotecaWeb.Controllers
{
    [SessionAuthorize]
    public class HomeController : Controller
    {
        private readonly GestionBibliotecaContext _context;

        public HomeController(GestionBibliotecaContext context)
        {
            _context = context;
        }

        public IActionResult Index(string buscar)
        {
            ViewBag.TotalLibros = _context.Libros.Count();

            ViewBag.TotalAutores = _context.Autores.Count();

            ViewBag.TotalLectores = _context.Lectores.Count();

            ViewBag.TotalPrestamos = _context.Prestamos.Count();

            ViewBag.TotalUsuarios = _context.Usuarios.Count();

            var libros = _context.Libros
                .Include(l => l.Autor)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                libros = libros.Where(l =>
                    l.Titulo.Contains(buscar) ||
                    l.Autor.Nombre.Contains(buscar)
                );
            }

            return View(libros.ToList());
        }
    }
}