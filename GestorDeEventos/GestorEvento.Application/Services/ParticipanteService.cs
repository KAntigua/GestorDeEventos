using GestorEvento.Application.DTOs;
using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Core;
using GestorEvento.Infrastructure.Interfaces;

namespace GestorEvento.Application.Services
{
    public class ParticipanteService
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly UnitOfWork _unitOfWork;

        public ParticipanteService(IParticipanteRepository participanteRepository, UnitOfWork unitOfWork)
        {
            _participanteRepository = participanteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ParticipanteDTO>> GetAllAsync()
        {
            var participantes = await _participanteRepository.GetAllParticipantesAsync();
            return participantes.Select(p => new ParticipanteDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Correo = p.Correo
            }).ToList();
        }

        public async Task<ParticipanteDTO> GetByIdAsync(int id)
        {
            var participante = await _participanteRepository.GetParticipanteByIdAsync(id);
            if (participante == null) return null;

            return new ParticipanteDTO
            {
                Id = participante.Id,
                Nombre = participante.Nombre,
                Correo = participante.Correo
            };
        }

        public async Task<int> CreateAsync(ParticipanteDTO model)
        {
            var entity = new Participante
            {
                Nombre = model.Nombre,
                Correo = model.Correo
            };

            await _unitOfWork.BeginTransactionAsync();
            await _participanteRepository.AddParticipanteAsync(entity); 
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return entity.Id;  
        }

        public async Task<bool> UpdateAsync(int id, ParticipanteDTO model)
        {
            var entity = new Participante
            {
                Id = id,
                Nombre = model.Nombre,
                Correo = model.Correo
            };

            await _unitOfWork.BeginTransactionAsync();

            var result = await _participanteRepository.UpdateParticipanteAsync(entity);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return result;  
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _unitOfWork.BeginTransactionAsync();

            
            var result = await _participanteRepository.DeleteParticipanteAsync(id); 

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return result;
        }
    }
}

