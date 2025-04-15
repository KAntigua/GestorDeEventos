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
        [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser mayor que 0")]
        public int Capacidad { get; set; }

        [Required(ErrorMessage = "el Precio la sala es requerida")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
        public decimal Precio { get; set; }
    }
}
