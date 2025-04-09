using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Application.DTOs
{
    public class SalaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la sala es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La capacidad la sala es requerida")]
        public int Capacidad { get; set; }

        [Required(ErrorMessage = "La capacidad la sala es requerida")]
        public decimal Precio { get; set; }
    }
}
