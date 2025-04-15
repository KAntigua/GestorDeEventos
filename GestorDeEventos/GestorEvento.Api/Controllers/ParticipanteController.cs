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
    public class ParticipanteController : ControllerBase
    {
        private readonly ParticipanteService _participanteService;

        public ParticipanteController(ParticipanteService participanteService)
        {
            _participanteService = participanteService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateParticipante([FromBody] ParticipanteDTO participanteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _participanteService.CreateAsync(participanteDTO);

            if (result == 0)
                return StatusCode(500, new { mensaje = "Ocurrió un error al crear el participante." });

            return Ok(new { mensaje = "Participante creado correctamente.", id = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParticipantes()
        {
            var participantes = await _participanteService.GetAllAsync();

            if (participantes == null || participantes.Count == 0)
                return NotFound(new { mensaje = "No se encontraron participantes." });

            return Ok(participantes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipanteById(int id)
        {
            var participante = await _participanteService.GetByIdAsync(id);

            if (participante == null)
                return NotFound(new { mensaje = $"No se encontró un participante con el ID {id}." });

            return Ok(participante);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParticipante(int id, [FromBody] ParticipanteDTO participanteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _participanteService.UpdateAsync(id, participanteDTO);

            if (!updated)
                return NotFound(new { mensaje = $"No se pudo actualizar el participante con ID {id}." });

            return Ok(new { mensaje = "Participante actualizado correctamente." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipante(int id)
        {
            var deleted = await _participanteService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { mensaje = $"No se pudo eliminar el participante con ID {id}." });

            return Ok(new { mensaje = "Participante eliminado correctamente." });
        }
    }
}
