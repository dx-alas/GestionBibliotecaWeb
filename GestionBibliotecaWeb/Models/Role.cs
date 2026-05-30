using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionBibliotecaWeb.Models;

public partial class Role
{
    public int RolId { get; set; }
    
    [Required(ErrorMessage = "El Nombre del Rol es obligatorio")]
    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
