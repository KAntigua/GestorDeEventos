
using GestorEvento.Application.DTOs;
using GestorEvento.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace GestorEvento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _usuarioService.CreateAsync(dto);
            return Ok(new { success = true, id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _usuarioService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound();
            return Ok(new { success = true });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _usuarioService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return Ok(new { success = true });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO loginDto)
        {
            var usuario = await _usuarioService.AuthenticateAsync(loginDto.Correo, loginDto.Clave);
            if (usuario == null)
                return Unauthorized("Correo o clave incorrectos");
            return Ok(usuario);
        }
    }
}
