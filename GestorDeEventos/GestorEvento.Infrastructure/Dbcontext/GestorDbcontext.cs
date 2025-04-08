
using Microsoft.EntityFrameworkCore;
using GestorEvento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Infrastructure.Persistence
{
    
    
        public class GestorDbcontext : DbContext
        {

            public GestorDbcontext(DbContextOptions<GestorDbcontext> options) : base(options)
            {

            }


            public DbSet<Usuario> Usuarios { get; set; }
            public DbSet<Sala> Salas { get; set; }
            public DbSet<Participante> Participantes { get; set; }        

            public DbSet<Evento> Eventos { get; set; }
            public DbSet<EventoParticipante> EventoParticipantes { get; set; }

            public async Task GetUsuarioById(int id)
            {
                throw new NotImplementedException();
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Usuario>().ToTable("Usuario");
                modelBuilder.Entity<Sala>().ToTable("Sala");
                modelBuilder.Entity<Participante>().ToTable("Participante");
                modelBuilder.Entity<EventoParticipante>().ToTable("EventoParticipante");
                modelBuilder.Entity<Evento>().ToTable("Evento");
            }

        }       
    
}
