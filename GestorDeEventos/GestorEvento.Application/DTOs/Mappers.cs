using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GestorEvento.Domain.Entities;


namespace GestorEvento.Application.DTOs
{
    public class Mappers
    {

        public static MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sala, SalaDTO>();
                cfg.CreateMap<SalaDTO, Sala>();

                cfg.CreateMap<Evento, EventoDTO>();
                cfg.CreateMap<EventoDTO, Evento>();

                cfg.CreateMap<Participante, ParticipanteDTO>();
                cfg.CreateMap<ParticipanteDTO, Participante>();

                cfg.CreateMap<Usuario, UsuarioDTO>();
                cfg.CreateMap<UsuarioDTO, Usuario>();

                cfg.CreateMap<EventoParticipante, EventoParticipanteDTO>();
                cfg.CreateMap<EventoParticipanteDTO, EventoParticipante>();
            }
            );

        }


    }
}
