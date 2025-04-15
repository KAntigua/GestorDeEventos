using Microsoft.EntityFrameworkCore;
using GestorEvento.Application.DTOs;
using AutoMapper;
using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using GestorEvento.Application.Services;


namespace GestorEvento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoParticipanteController : ControllerBase
    {
        private readonly EventoParticipanteService _eventoParticipanteService;

        public EventoParticipanteController(EventoParticipanteService eventoParticipanteService)
        {
            _eventoParticipanteService = eventoParticipanteService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventoParticipante([FromBody] EventoParticipanteDTO eventoParticipanteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _eventoParticipanteService.CreateAsync(eventoParticipanteDTO);

                if (result == 0)
                    return StatusCode(500, new { success = false, message = "Ocurrió un error al crear la relación Evento-Participante." });

                return Ok(new { success = true, message = "Relación creada exitosamente.", id = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error interno: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventoParticipantes()
        {
            try
            {
                var relaciones = await _eventoParticipanteService.GetAllAsync();

                if (relaciones == null || relaciones.Count == 0)
                    return NotFound(new { success = false, message = "No se encontraron relaciones de evento y participante." });

                return Ok(new { success = true, data = relaciones });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error al obtener datos: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoParticipanteById(int id)
        {
            try
            {
                var relacion = await _eventoParticipanteService.GetByIdAsync(id);

                if (relacion == null)
                    return NotFound(new { success = false, message = $"No se encontró la relación con ID {id}." });

                return Ok(new { success = true, data = relacion });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error al obtener la relación: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEventoParticipante(int id, [FromBody] EventoParticipanteDTO eventoParticipanteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _eventoParticipanteService.UpdateAsync(id, eventoParticipanteDTO);

                if (!updated)
                    return NotFound(new { success = false, message = $"No se pudo actualizar la relación con ID {id}." });

                return Ok(new { success = true, message = "Relación actualizada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error al actualizar: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventoParticipante(int id)
        {
            try
            {
                var deleted = await _eventoParticipanteService.DeleteAsync(id);

                if (!deleted)
                    return NotFound(new { success = false, message = $"No se pudo eliminar la relación con ID {id}." });

                return Ok(new { success = true, message = "Relación eliminada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error al eliminar: {ex.Message}" });
            }
        }
    }
}
