using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Persistence;
using GestorEvento.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace GestorEvento.Infrastructure.Repositories
{
    public class RegistrarParticipanteRepository : IRegistrarParticipanteRepository
    {
        private readonly GestorDbcontext _context;

        public RegistrarParticipanteRepository(GestorDbcontext context)
        {
            _context = context;
        }

        public async Task<List<Participante>> GetAllAsync()
        {
            return await _context.Participantes.ToListAsync();
        }

        public async Task<Participante> GetByIdAsync(int id)
        {
            return await _context.Participantes.FindAsync(id);
        }

        public async Task<int> AddParticipanteAsync(Participante participante, int eventoId)
        {
            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();

            var eventoParticipante = new EventoParticipante
            {
                ParticipanteId = participante.Id,
                EventoId = eventoId,
                FechaRegistro = DateTime.UtcNow
            };

            _context.EventoParticipantes.Add(eventoParticipante);
            await _context.SaveChangesAsync();

            return participante.Id;
        }

        public async Task<bool> DeleteParticipanteAsync(int id)
        {
            var entity = await _context.Participantes.FindAsync(id);
            if (entity == null) return false;

            _context.Participantes.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
