using GestorEvento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Application.DTOs
{
    public class EventoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del evento es requerido")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "La descripcion del evento es requerida")]
        public string? Descripcion { get; set; }


        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        public DateTime FechaInicio { get; set; }


        [Required(ErrorMessage = "La fecha de final es requerida")]
        public DateTime FechaFin { get; set; }


        [Required(ErrorMessage = "La Hora de inicio es requerido")]
        public TimeSpan HoraInicio { get; set; }


        [Required(ErrorMessage = "La Hora de inicio es requerido")]
        public TimeSpan HoraFin { get; set; }


        [Required(ErrorMessage = "El ID de la sala es requerido")]
        public int SalaId { get; set; }
  
    }

}

