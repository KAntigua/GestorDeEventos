using GestorEvento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Infrastructure.Interfaces
{
    public interface ISalaRepository
    {
        Task<Sala> GetByIdAsync(int id);
        Task<List<Sala>> GetAllAsync();
        Task<int> AddSalaAsync(Sala sala);
        Task<bool> UpdateSalaAsync(Sala sala);
        Task<bool> DeleteSalaAsync(int id);
    }

}
