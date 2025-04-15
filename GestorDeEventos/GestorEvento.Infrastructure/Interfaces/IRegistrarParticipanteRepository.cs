using GestorEvento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Infrastructure.Interfaces
{
    public interface IRegistrarParticipanteRepository
    {
        Task<List<Participante>> GetAllAsync();
        Task<Participante> GetByIdAsync(int id);
        Task<int> AddParticipanteAsync(Participante participante, int eventoId);
        Task<bool> DeleteParticipanteAsync(int id);
    }
}
