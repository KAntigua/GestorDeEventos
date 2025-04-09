using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Application.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "El correo debe tener el formato válido de @gmail.com")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El clave es requerido")]
        [MinLength(8, ErrorMessage = "La clave debe tener al menos 8 caracteres")]
        public string Clave { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        [RegularExpression("^(Usuario|Administrador)$", ErrorMessage = "El rol debe ser 'Usuario' o 'Administrador'")]
        public string Rol { get; set; }
    }
}
