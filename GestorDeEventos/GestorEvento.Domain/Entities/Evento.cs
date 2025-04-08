using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Domain.Entities
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        
        public int SalaId { get; set; }
        public Sala Sala { get; set; }

       
        public ICollection<EventoParticipante> EventoParticipantes { get; set; }
    }
}
