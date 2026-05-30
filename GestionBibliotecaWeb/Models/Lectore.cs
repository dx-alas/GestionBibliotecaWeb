using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionBibliotecaWeb.Models;

public partial class Lectore
{
    public int LectorId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    [Required(ErrorMessage = "El DUI es obligatorio")]
    [RegularExpression(@"^\d{8}-\d{1}$",
    ErrorMessage = "El formato del DUI debe ser 00000000-0")]
    public string Documento { get; set; } = null!;

    public string? Correo { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [StringLength(9)]
    [RegularExpression(@"^\d{4}-\d{4}$",
    ErrorMessage = "El formato debe ser 0000-0000")]
    public string? Telefono { get; set; }

    public string NombreCompleto
    {
        get
        {
            return LectorId + " - " + Nombre;
        }
    }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
