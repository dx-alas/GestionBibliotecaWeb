using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionBibliotecaWeb.Models;

public partial class Libro
{
    public int LibroId { get; set; }

    [Required(ErrorMessage = "El Nombre del Libro es obligatorio")]
    public string Titulo { get; set; } = null!;

    [Required(ErrorMessage = "El Autor del Libro es obligatorio")]
    public int AutorId { get; set; }

    [Required(ErrorMessage = "El ISBN es obligatorio")]
    [RegularExpression(
    @"^(978|979)-\d{2}-\d{3}-\d{4}-\d$",
    ErrorMessage = "Formato ISBN inválido"
)]
    public string Isbn { get; set; } = null!;

    [ValidateNever]
    public virtual Autore Autor { get; set; } = null!;

    public string? ImagenUrl { get; set; }

    public string NombreCompleto
    {
        get
        {
            return LibroId + " - " + Titulo;
        }
    }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
