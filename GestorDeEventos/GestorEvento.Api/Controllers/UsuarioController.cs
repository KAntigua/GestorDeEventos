using Microsoft.EntityFrameworkCore;
using GestorEvento.Application.DTOs;
using AutoMapper;
using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
namespace GestorEvento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly GestorDbcontext _context;

        public UsuarioController(IMapper mapper, GestorDbcontext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                return BadRequest("Los datos del usuario no pueden ser nulos.");
            }

           
            var usuarioExistente = await _context.Usuarios
                                                  .FirstOrDefaultAsync(u => u.Correo == usuarioDTO.Correo);
            if (usuarioExistente != null)
            {
                return Conflict("Ya existe un usuario con el mismo correo.");
            }
            var usuario = _mapper.Map<Usuario>(usuarioDTO);

            try
            {
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();

                var usuarioCreadoDTO = _mapper.Map<UsuarioDTO>(usuario);
                return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id }, usuarioCreadoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el usuario: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();

                if (usuarios == null || usuarios.Count == 0)
                {
                    return NotFound("No se encontraron usuarios.");
                }

                var usuariosDTO = _mapper.Map<List<UsuarioDTO>>(usuarios);
                return Ok(usuariosDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los usuarios: {ex.Message}");
            }
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            try
            {
                var usuario = await _context.Usuarios
                                             .FirstOrDefaultAsync(u => u.Id == id);

                if (usuario == null)
                {
                    return NotFound($"No se encontró un usuario con el ID {id}.");
                }

                var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
                return Ok(usuarioDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el usuario: {ex.Message}");
            }
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                return BadRequest("Los datos del usuario no pueden ser nulos.");
            }

            var usuarioExistente = await _context.Usuarios.FindAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound($"No se encontró un usuario con el ID {id}.");
            }

            try
            {
                usuarioExistente.Nombre = usuarioDTO.Nombre;
                usuarioExistente.Correo = usuarioDTO.Correo;
                usuarioExistente.Clave = usuarioDTO.Clave;
                usuarioExistente.Rol = usuarioDTO.Rol;

                await _context.SaveChangesAsync();

                var usuarioActualizadoDTO = _mapper.Map<UsuarioDTO>(usuarioExistente);
                return Ok(usuarioActualizadoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el usuario: {ex.Message}");
            }
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound($"No se encontró un usuario con el ID {id}.");
            }

            try
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                return Ok($"Usuario con ID {id} eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el usuario: {ex.Message}");
            }
        }
    }
}
