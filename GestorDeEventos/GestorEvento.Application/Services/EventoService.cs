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
    public class EventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly UnitOfWork _unitOfWork;

        public EventoService(IEventoRepository eventoRepository, UnitOfWork unitOfWork)
        {
            _eventoRepository = eventoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<EventoDTO>> GetAllAsync()
        {
            var eventos = await _eventoRepository.GetAllAsync();

            return eventos.Select(e => new EventoDTO
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,
                FechaInicio = e.FechaInicio,
                FechaFin = e.FechaFin,
                HoraInicio = e.HoraInicio,
                HoraFin = e.HoraFin,
                SalaId = e.SalaId
            }).ToList();
        }

        public async Task<EventoDTO> GetByIdAsync(int id)
        {
            var e = await _eventoRepository.GetByIdAsync(id);
            if (e == null) return null;

            return new EventoDTO
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,
                FechaInicio = e.FechaInicio,
                FechaFin = e.FechaFin,
                HoraInicio = e.HoraInicio,
                HoraFin = e.HoraFin,
                SalaId = e.SalaId
            };
        }

        public async Task<int> CreateAsync(EventoDTO model)
        {
            var entity = new Evento
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                FechaInicio = model.FechaInicio,
                FechaFin = model.FechaFin,
                HoraInicio = model.HoraInicio,
                HoraFin = model.HoraFin,
                SalaId = model.SalaId
            };

            await _unitOfWork.BeginTransactionAsync();
            var id = await _eventoRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return id;
        }

        public async Task<bool> UpdateAsync(int id, EventoDTO model)
        {
            var entity = new Evento
            {
                Id = id,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                FechaInicio = model.FechaInicio,
                FechaFin = model.FechaFin,
                HoraInicio = model.HoraInicio,
                HoraFin = model.HoraFin,
                SalaId = model.SalaId
            };

            await _unitOfWork.BeginTransactionAsync();
            var result = await _eventoRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            var result = await _eventoRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return result;
        }
    }
}
