using GestorEvento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Infrastructure.Interfaces
{
    public interface IEventoParticipanteRepository
    {
        Task<EventoParticipante> GetByIdAsync(int id);
        Task<List<EventoParticipante>> GetAllAsync();
        Task<int> AddAsync(EventoParticipante eventoParticipante);
        Task<bool> UpdateAsync(EventoParticipante eventoParticipante);
        Task<bool> DeleteAsync(int id);
    }
}
