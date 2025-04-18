using GestorEvento.Domain.Entities;

namespace GestorEvento.Web.Models
{
    public class SalaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public decimal Precio { get; set; }


    }
}
