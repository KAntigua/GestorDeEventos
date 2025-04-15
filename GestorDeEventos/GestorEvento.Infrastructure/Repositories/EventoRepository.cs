using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Core;
using GestorEvento.Infrastructure.Interfaces;
using GestorEvento.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Infrastructure.Repositories
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRepository
    {
        public EventoRepository(GestorDbcontext context) : base(context) { }

        public async Task<Evento> GetByIdAsync(int id)
        {
            return await Context.Eventos
                .Include(e => e.Sala)
                .Include(e => e.EventoParticipantes)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Evento>> GetAllAsync()
        {
            return await Context.Eventos
                .Include(e => e.Sala)
                .Include(e => e.EventoParticipantes)
                .ToListAsync();
        }

        public async Task<int> AddAsync(Evento evento)
        {
            await Context.Eventos.AddAsync(evento);
            await Context.SaveChangesAsync();
            return evento.Id;
        }

        public async Task<bool> UpdateAsync(Evento evento)
        {
            Context.Eventos.Update(evento);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await Context.Eventos.FindAsync(id);
            if (entity == null) return false;

            Context.Eventos.Remove(entity);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
