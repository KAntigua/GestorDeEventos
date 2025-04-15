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
    public class ParticipanteRepository : BaseRepository<Participante>, IParticipanteRepository
    {
        public ParticipanteRepository(GestorDbcontext context) : base(context) { }

        public async Task<Participante> GetParticipanteByIdAsync(int id)
        {
            var participante = await Context.Participantes
                                             .Where(p => p.Id == id)
                                             .FirstOrDefaultAsync();

            return participante;  
        }

        public async Task<IEnumerable<Participante>> GetAllParticipantesAsync()
        {
            return await Context.Participantes
                                 .Select(p => new Participante
                                 {
                                     Id = p.Id,
                                     Nombre = p.Nombre,
                                     Correo = p.Correo
                                 })
                                 .ToListAsync();
        }

        public async Task AddParticipanteAsync(Participante participante)
        {
            await Context.Participantes.AddAsync(participante);
            await Context.SaveChangesAsync();
        }

       
        public async Task<bool> UpdateParticipanteAsync(Participante entity)
        {
            var participante = await Context.Participantes.FindAsync(entity.Id);
            if (participante == null)
            {
                return false; 
            }

            participante.Nombre = entity.Nombre;
            participante.Correo = entity.Correo;

            Context.Participantes.Update(participante);
            await Context.SaveChangesAsync();

            return true; 
        }

       
        public async Task<bool> DeleteParticipanteAsync(int id)
        {
            var participante = await Context.Participantes
                                             .Where(p => p.Id == id)
                                             .FirstOrDefaultAsync();

            if (participante == null)
            {
                return false; 
            }

            Context.Participantes.Remove(participante);
            await Context.SaveChangesAsync();

            return true; 
        }
    }
}
