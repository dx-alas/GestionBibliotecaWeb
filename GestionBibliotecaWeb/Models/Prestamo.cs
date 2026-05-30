using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GestionBibliotecaWeb.Models;

public partial class Prestamo
{
    public int PrestamoId { get; set; }

    public int LibroId { get; set; }

    public int LectorId { get; set; }

    [Required(ErrorMessage = "La Fecha Prestamo es obligatoro")]
    public DateTime FechaPrestamo { get; set; }

    [Required(ErrorMessage = "La Fecha Devolución es obligatorio")]
    public DateTime? FechaDevolucion { get; set; }

    [Required(ErrorMessage = "La Observación es obligatorio")]
    public string? Observacion { get; set; }

    public int UsuarioId { get; set; }

    [ValidateNever]
    public virtual Lectore Lector { get; set; } = null!;

    [ValidateNever]
    public virtual Libro Libro { get; set; } = null!;

    [ValidateNever]
    public virtual Usuario Usuario { get; set; } = null!;
}