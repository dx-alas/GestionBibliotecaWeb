using GestionBibliotecaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionBibliotecaWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly GestionBibliotecaContext _context;

        public LoginController(GestionBibliotecaContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Index()
        {
            // Si ya inició sesión
            if (HttpContext.Session.GetString("Usuario") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string nombre, string clave)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre == nombre && u.Activo == true);

            if (usuario != null)
            {
                bool claveCorrecta = BCrypt.Net.BCrypt.Verify(clave, usuario.Clave);

                if (claveCorrecta)
                {
                    HttpContext.Session.SetString("Usuario", usuario.Nombre);
                    HttpContext.Session.SetInt32("UsuarioId", usuario.UsuarioId);

                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";

            return View();
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
    }
}