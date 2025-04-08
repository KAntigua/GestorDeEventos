using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Domain.Entities
{
    public class Participante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        
        public ICollection<EventoParticipante> EventoParticipantes { get; set; }
    }
}
