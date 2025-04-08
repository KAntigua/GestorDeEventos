using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Domain.Entities
{
    public class EventoParticipante
    {
        public int Id { get; set; }
        public Evento Evento { get; set; }

        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
