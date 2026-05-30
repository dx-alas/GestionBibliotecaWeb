using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GestionBibliotecaWeb.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Clave { get; set; }

    public int RolId { get; set; }

    public bool Activo { get; set; }

    [ValidateNever]
    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    [ValidateNever]
    public virtual Role Rol { get; set; } = null!;

    public string NombreCompleto
    {
        get
        {
            return UsuarioId + " - " + Nombre;
        }
    }
}