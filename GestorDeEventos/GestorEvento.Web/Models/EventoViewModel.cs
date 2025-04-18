using GestorEvento.Domain.Entities;

namespace GestorEvento.Web.Models
{
    public class EventoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        public int SalaId { get; set; }
        public string SalaNombre { get; set; }
    }
}
