using Microsoft.AspNetCore.Mvc;
using GestorEvento.Application.DTOs;
using GestorEvento.Application.Services;


namespace GestorEvento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaController : ControllerBase
    {
        private readonly SalaService _salaService;

        public SalaController(SalaService salaService)
        {
            _salaService = salaService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSala([FromBody] SalaDTO salaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var id = await _salaService.CreateAsync(salaDTO);
                var createdSala = await _salaService.GetByIdAsync(id);

                return CreatedAtAction(nameof(GetSalaById), new { id = id }, createdSala);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la sala: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalas()
        {
            try
            {
                var salas = await _salaService.GetAllAsync();

                if (salas == null || !salas.Any())
                    return NotFound("No se encontraron salas.");

                return Ok(salas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las salas: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalaById(int id)
        {
            try
            {
                var sala = await _salaService.GetByIdAsync(id);
                if (sala == null)
                    return NotFound($"No se encontró una sala con el ID {id}.");

                return Ok(sala);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSala(int id, [FromBody] SalaDTO salaDTO)
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return BadRequest(new { mensaje = "Datos inválidos", errores });
            }

            try
            {
                
                var salaExistente = await _salaService.GetByIdAsync(id);
                if (salaExistente == null)
                    return NotFound($"No se encontró una sala con el ID {id}.");

                
                var updated = await _salaService.UpdateAsync(id, salaDTO);
                if (!updated)
                    return StatusCode(500, "Error al actualizar la sala.");

                var updatedSala = await _salaService.GetByIdAsync(id);
                return Ok(new { mensaje = "Sala actualizada exitosamente", sala = updatedSala });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la sala: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            try
            {
                var deleted = await _salaService.DeleteAsync(id);
                if (!deleted)
                    return NotFound($"No se encontró una sala con el ID {id}.");

                return Ok($"Sala con ID {id} eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la sala: {ex.Message}");
            }
        }
    }
}
