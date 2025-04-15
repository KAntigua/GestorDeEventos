
using GestorEvento.Application.DTOs;
using GestorEvento.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace GestorEvento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _usuarioService.CreateAsync(usuarioDTO);

            if (result == 0)
                return StatusCode(500, "Ocurrió un error al crear el usuario.");

            return Ok(new { success = true, id = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            var usuarios = await _usuarioService.GetAllAsync();

            if (usuarios == null || usuarios.Count == 0)
                return NotFound("No se encontraron usuarios.");

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);

            if (usuario == null)
                return NotFound($"No se encontró un usuario con el ID {id}.");

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _usuarioService.UpdateAsync(id, usuarioDTO);

            if (!updated)
                return NotFound($"No se pudo actualizar el usuario con ID {id}.");

            return Ok(new { success = true });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var deleted = await _usuarioService.DeleteAsync(id);

            if (!deleted)
                return NotFound($"No se pudo eliminar el usuario con ID {id}.");

            return Ok(new { success = true });
        }
    }

}
