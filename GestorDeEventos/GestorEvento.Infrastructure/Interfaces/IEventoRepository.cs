using GestorEvento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Infrastructure.Interfaces
{
    public interface IEventoRepository
    {
        Task<Evento> GetByIdAsync(int id);
        Task<List<Evento>> GetAllAsync();
        Task<int> AddAsync(Evento evento);
        Task<bool> UpdateAsync(Evento evento);
        Task<bool> DeleteAsync(int id);
    }
}
