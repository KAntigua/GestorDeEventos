using Microsoft.EntityFrameworkCore;
using GestorEvento.Application.DTOs;
using AutoMapper;
using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using GestorEvento.Application.Services;

namespace GestorEvento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly EventoService _eventoService;

        public EventoController(EventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvento([FromBody] EventoDTO eventoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var id = await _eventoService.CreateAsync(eventoDTO);

                if (id == 0)
                    return StatusCode(500, "Ocurrió un error al crear el evento.");

                return Ok(new { success = true, message = "Evento creado exitosamente", id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventos()
        {
            var eventos = await _eventoService.GetAllAsync();

            if (eventos == null || eventos.Count == 0)
                return NotFound("No se encontraron eventos registrados.");

            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoById(int id)
        {
            var evento = await _eventoService.GetByIdAsync(id);

            if (evento == null)
                return NotFound($"No se encontró el evento con ID {id}.");

            return Ok(evento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvento(int id, [FromBody] EventoDTO eventoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _eventoService.UpdateAsync(id, eventoDTO);

                if (!updated)
                    return NotFound($"No se pudo actualizar el evento con ID {id}.");

                return Ok(new { success = true, message = "Evento actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el evento: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            try
            {
                var deleted = await _eventoService.DeleteAsync(id);

                if (!deleted)
                    return NotFound($"No se pudo eliminar el evento con ID {id}.");

                return Ok(new { success = true, message = "Evento eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el evento: {ex.Message}");
            }
        }
    }

}

