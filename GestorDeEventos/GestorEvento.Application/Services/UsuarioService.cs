using BCrypt.Net;
using GestorEvento.Application.DTOs;
using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Core;
using GestorEvento.Infrastructure.Interfaces;



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
            var usuariosDTO = new List<UsuarioDTO>();

            foreach (var usuario in usuarios)
            {
                usuariosDTO.Add(new UsuarioDTO
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Correo = usuario.Correo,
                    Rol = usuario.Rol
                });
            }

            return usuariosDTO;
        }

        
        public async Task<UsuarioDTO> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null) return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Rol = usuario.Rol
            };
        }


        public async Task<int> CreateAsync(UsuarioDTO model)
        {
            var entity = new Usuario
            {
                Nombre = model.Nombre,
                Correo = model.Correo,
                Clave = BCrypt.Net.BCrypt.HashPassword(model.Clave), 
                Rol = model.Rol
            };

            await _unitOfWork.BeginTransactionAsync();
            var id = await _usuarioRepository.AddUsuarioAsync(entity);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return id;
        }


        public async Task<bool> UpdateAsync(int id, UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario
            {
                Id = id,
                Nombre = usuarioDTO.Nombre,
                Correo = usuarioDTO.Correo,
                Clave = usuarioDTO.Clave,  
                Rol = usuarioDTO.Rol
            };

            return await _usuarioRepository.UpdateUsuarioAsync(usuario);
        }

       
        public async Task<bool> DeleteAsync(int id)
        {
            return await _usuarioRepository.DeleteUsuarioAsync(id);
        }

       
        public async Task<UsuarioDTO> AuthenticateAsync(string correo, string clave)
        {
            var usuario = await _usuarioRepository.GetByCorreoAsync(correo);

            if (usuario == null)
                return null;

            
            if (!BCrypt.Net.BCrypt.Verify(clave, usuario.Clave))
                return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Rol = usuario.Rol
            };
        }
    }



}

