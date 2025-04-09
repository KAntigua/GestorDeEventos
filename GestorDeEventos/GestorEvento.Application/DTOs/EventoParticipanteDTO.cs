using GestorEvento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Application.DTOs
{
    public class EventoParticipanteDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID del evento es requerido")]
        public int EventoId { get; set; }

        [Required(ErrorMessage = "El ID del participante es requerido")]
        public int ParticipanteId { get; set; }

        [Required(ErrorMessage = "La fecha de registro es requerida")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
