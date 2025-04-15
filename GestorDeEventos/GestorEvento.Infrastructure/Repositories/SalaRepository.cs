using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Core;
using GestorEvento.Infrastructure.Persistence;
using GestorEvento.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorEvento.Infrastructure.Repositories
{
    public class SalaRepository : BaseRepository<Sala>, ISalaRepository
    {
        public SalaRepository(GestorDbcontext context) : base(context) { }

        public async Task<Sala> GetByIdAsync(int id)
        {
            return await Context.Salas.FindAsync(id);
        }

        public async Task<List<Sala>> GetAllAsync()
        {
            return await Context.Salas.ToListAsync();
        }

        public async Task<int> AddSalaAsync(Sala sala)
        {
            return await AddAsync(sala);
        }

        public async Task<bool> UpdateSalaAsync(Sala sala)
        {
            return await UpdateAsync(sala);
        }

        public async Task<bool> DeleteSalaAsync(int id)
        {
            return await DeleteAsync(id);
        }
    }
}
