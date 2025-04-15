using GestorEvento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Infrastructure.Interfaces
{
    public interface IParticipanteRepository
    {
        Task<Participante> GetParticipanteByIdAsync(int id);
        Task<IEnumerable<Participante>> GetAllParticipantesAsync();
        Task AddParticipanteAsync(Participante participante);
        Task<bool> UpdateParticipanteAsync(Participante participante);
        Task<bool> DeleteParticipanteAsync(int id);
    }
}
