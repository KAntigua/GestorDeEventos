using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Domain.Entities
{
    public class Sala
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public decimal Precio { get; set; }
        

     
        public ICollection<Evento> Eventos { get; set; }
    }
}
