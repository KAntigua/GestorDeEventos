using GestorEvento.Application.DTOs;
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
    public class EventoParticipanteService
    {
        private readonly IEventoParticipanteRepository _eventoParticipanteRepository;
        private readonly UnitOfWork _unitOfWork;

        public EventoParticipanteService(IEventoParticipanteRepository eventoParticipanteRepository, UnitOfWork unitOfWork)
        {
            _eventoParticipanteRepository = eventoParticipanteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<EventoParticipanteDTO>> GetAllAsync()
        {
            var relaciones = await _eventoParticipanteRepository.GetAllAsync();

            return relaciones.Select(r => new EventoParticipanteDTO
            {
                Id = r.Id,
                EventoId = r.EventoId,
                ParticipanteId = r.ParticipanteId,
                FechaRegistro = r.FechaRegistro
            }).ToList();
        }

        public async Task<EventoParticipanteDTO> GetByIdAsync(int id)
        {
            var r = await _eventoParticipanteRepository.GetByIdAsync(id);
            if (r == null) return null;

            return new EventoParticipanteDTO
            {
                Id = r.Id,
                EventoId = r.EventoId,
                ParticipanteId = r.ParticipanteId,
                FechaRegistro = r.FechaRegistro
            };
        }

        public async Task<int> CreateAsync(EventoParticipanteDTO model)
        {
            var entity = new EventoParticipante
            {
                EventoId = model.EventoId,
                ParticipanteId = model.ParticipanteId,
                FechaRegistro = model.FechaRegistro
            };

            await _unitOfWork.BeginTransactionAsync();
            var id = await _eventoParticipanteRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return id;
        }

        public async Task<bool> UpdateAsync(int id, EventoParticipanteDTO model)
        {
            var entity = new EventoParticipante
            {
                Id = id,
                EventoId = model.EventoId,
                ParticipanteId = model.ParticipanteId,
                FechaRegistro = model.FechaRegistro
            };

            await _unitOfWork.BeginTransactionAsync();
            var result = await _eventoParticipanteRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            var result = await _eventoParticipanteRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return result;
        }
    }
}
