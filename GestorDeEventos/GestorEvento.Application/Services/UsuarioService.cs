using GestorEvento.Application.DTOs;
using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Core;
using GestorEvento.Infrastructure.Interfaces;
using GestorEvento.Infrastructure.Repositories;


namespace GestorEvento.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UnitOfWork _unitOfWork;

        public UsuarioService(IUsuarioRepository usuarioRepository, UnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Correo = u.Correo,
                Clave = u.Clave,
                Rol = u.Rol
            }).ToList();
        }

        public async Task<UsuarioDTO> GetByIdAsync(int id)
        {
            var u = await _usuarioRepository.GetByIdAsync(id);
            if (u == null) return null;

            return new UsuarioDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Correo = u.Correo,
                Clave = u.Clave,
                Rol = u.Rol
            };
        }

        public async Task<int> CreateAsync(UsuarioDTO model)
        {
            var entity = new Usuario
            {
                Nombre = model.Nombre,
                Correo = model.Correo,
                Clave = model.Clave,
                Rol = model.Rol
            };

            await _unitOfWork.BeginTransactionAsync();
            var id = await _usuarioRepository.AddUsuarioAsync(entity);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return id;
        }

        public async Task<bool> UpdateAsync(int id, UsuarioDTO model)
        {
            var entity = new Usuario
            {
                Id = id,
                Nombre = model.Nombre,
                Correo = model.Correo,
                Clave = model.Clave,
                Rol = model.Rol
            };

            await _unitOfWork.BeginTransactionAsync();
            var result = await _usuarioRepository.UpdateUsuarioAsync(entity);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            var result = await _usuarioRepository.DeleteUsuarioAsync(id);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return result;
        }
    }
}
