using GestorEvento.Application.DTOs;
using GestorEvento.Application.Interfaces;
using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Core;
using GestorEvento.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Application.Services
{
    public class RegistrarParticipanteService : IRegistrarParticipanteService
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly UnitOfWork _unitOfWork;

        public RegistrarParticipanteService(IParticipanteRepository participanteRepository, UnitOfWork unitOfWork)
        {
            _participanteRepository = participanteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> RegistrarParticipanteAsync(RegistrarParticipanteDTO dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            var participanteExistente = await _participanteRepository
                .GetParticipanteByCorreoAsync(dto.Correo);

            Participante participante;

            if (participanteExistente == null)
            {
                participante = new Participante
                {
                    Nombre = dto.Nombre,
                    Correo = dto.Correo
                };

                await _participanteRepository.AddParticipanteAsync(participante);
                await _unitOfWork.CompleteAsync(); 
            }
            else
            {
                participante = participanteExistente;
            }

            var yaRegistrado = await _participanteRepository.ExisteEventoParticipanteAsync(participante.Id, dto.EventoId);
            if (yaRegistrado)
                throw new InvalidOperationException("Este participante ya está registrado en el evento.");

            var eventoParticipante = new EventoParticipante
            {
                ParticipanteId = participante.Id,
                EventoId = dto.EventoId,
                FechaRegistro = DateTime.Now
            };

            await _participanteRepository.AgregarEventoParticipanteAsync(eventoParticipante);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return participante.Id;
        }

    }
}
