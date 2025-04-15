using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Core;
using GestorEvento.Infrastructure.Persistence;
using GestorEvento.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace GestorEvento.Infrastructure.Repositories
{
    public class EventoParticipanteRepository : BaseRepository<EventoParticipante>, IEventoParticipanteRepository
    {
        public EventoParticipanteRepository(GestorDbcontext context) : base(context) { }

        public async Task<EventoParticipante> GetByIdAsync(int id)
        {
            return await Context.EventoParticipantes
                .Include(ep => ep.Evento)
                .Include(ep => ep.Participante)
                .FirstOrDefaultAsync(ep => ep.Id == id);
        }

        public async Task<List<EventoParticipante>> GetAllAsync()
        {
            return await Context.EventoParticipantes
                .Include(ep => ep.Evento)
                .Include(ep => ep.Participante)
                .ToListAsync();
        }

        public async Task<int> AddAsync(EventoParticipante eventoParticipante)
        {
            await Context.EventoParticipantes.AddAsync(eventoParticipante);
            await Context.SaveChangesAsync(); 
            return eventoParticipante.Id;
        }

        public async Task<bool> UpdateAsync(EventoParticipante eventoParticipante)
        {
            Context.EventoParticipantes.Update(eventoParticipante);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await Context.EventoParticipantes.FindAsync(id);
            if (entity == null) return false;

            Context.EventoParticipantes.Remove(entity);
            await Context.SaveChangesAsync();
            return true;
        }
    }

}
