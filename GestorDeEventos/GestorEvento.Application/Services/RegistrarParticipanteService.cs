using GestorEvento.Application.DTOs;
using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Core;
using GestorEvento.Infrastructure.Interfaces; 


namespace GestorEvento.Application.Services
{
    public class RegistrarParticipanteService
    {
        private readonly IRegistrarParticipanteRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        
        public RegistrarParticipanteService(IRegistrarParticipanteRepository repository, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateAsync(RegistrarParticipanteDTO model)
        {
            var participante = new Participante
            {
                Nombre = model.Nombre,
                Correo = model.Correo
            };

            await _unitOfWork.BeginTransactionAsync();

            var idParticipante = await _repository.AddParticipanteAsync(participante, model.EventoId);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return idParticipante;
        }
    }
}

