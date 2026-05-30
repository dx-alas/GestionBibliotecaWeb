using System;
using System.Collections.Generic;

namespace GestionBibliotecaWeb.Models;

public partial class Autore
{
    public int AutorId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();


    public string NombreCompleto
    {
        get
        {
            return AutorId + " - " + Nombre;
        }
    }

}