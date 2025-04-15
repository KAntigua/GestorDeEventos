using GestorEvento.Application.DTOs;
using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Core;
using GestorEvento.Infrastructure.Interfaces;
using GestorEvento.Infrastructure.Repositories;


namespace GestorEvento.Application.Services
{
    public class SalaService
    {
        private readonly ISalaRepository _salaRepository;
        private readonly UnitOfWork _unitOfWork;

        public SalaService(ISalaRepository salaRepository, UnitOfWork unitOfWork)
        {
            _salaRepository = salaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SalaDTO>> GetAllAsync()
        {
            var salas = await _salaRepository.GetAllAsync();
            return salas.Select(s => new SalaDTO
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Capacidad = s.Capacidad,
                Precio = s.Precio
            }).ToList();
        }

        public async Task<SalaDTO> GetByIdAsync(int id)
        {
            var sala = await _salaRepository.GetByIdAsync(id);
            if (sala == null) return null;

            return new SalaDTO
            {
                Id = sala.Id,
                Nombre = sala.Nombre,
                Capacidad = sala.Capacidad,
                Precio = sala.Precio
            };
        }

        public async Task<int> CreateAsync(SalaDTO model)
        {
            var entity = new Sala
            {
                Nombre = model.Nombre,
                Capacidad = model.Capacidad,
                Precio = model.Precio
            };

            await _unitOfWork.BeginTransactionAsync();
            var id = await _salaRepository.AddSalaAsync(entity);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return id;
        }

        public async Task<bool> UpdateAsync(int id, SalaDTO model)
        {
            var entity = new Sala
            {
                Id = id,
                Nombre = model.Nombre,
                Capacidad = model.Capacidad,
                Precio = model.Precio
            };

            await _unitOfWork.BeginTransactionAsync();
            var result = await _salaRepository.UpdateSalaAsync(entity);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            var result = await _salaRepository.DeleteSalaAsync(id);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return result;
        }
    }
}
