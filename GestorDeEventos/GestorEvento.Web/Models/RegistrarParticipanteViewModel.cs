using System.ComponentModel.DataAnnotations;

namespace GestorEvento.Web.Models
{
    public class RegistrarParticipanteViewModel
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un evento")]
        [Display(Name = "Evento")]
        public int EventoId { get; set; }
    }
}
