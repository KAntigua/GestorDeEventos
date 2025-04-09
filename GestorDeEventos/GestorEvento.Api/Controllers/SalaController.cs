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
    public class SalaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly GestorDbcontext _context;

        public SalaController(IMapper mapper, GestorDbcontext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSala([FromBody] SalaDTO salaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var sala = _mapper.Map<Sala>(salaDTO);

                await _context.Set<Sala>().AddAsync(sala);
                await _context.SaveChangesAsync();

                var salaCreadaDTO = _mapper.Map<SalaDTO>(sala);
                return CreatedAtAction(nameof(GetSalaById), new { id = sala.Id }, salaCreadaDTO);
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
                var salas = await _context.Set<Sala>().ToListAsync();

                if (salas == null || !salas.Any())
                {
                    return NotFound("No se encontraron salas.");
                }

                var salasDTO = _mapper.Map<List<SalaDTO>>(salas);
                return Ok(salasDTO);
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
                var sala = await _context.Set<Sala>().FindAsync(id);

                if (sala == null)
                    return NotFound($"No se encontró una sala con el ID {id}.");

                var salaDTO = _mapper.Map<SalaDTO>(sala);
                return Ok(salaDTO);
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
                return BadRequest(ModelState);
            }

            var salaExistente = await _context.Set<Sala>().FindAsync(id);
            if (salaExistente == null)
            {
                return NotFound($"No se encontró una sala con el ID {id}.");
            }

            try
            {
                _mapper.Map(salaDTO, salaExistente);
                await _context.SaveChangesAsync();

                var salaActualizadaDTO = _mapper.Map<SalaDTO>(salaExistente);
                return Ok(salaActualizadaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la sala: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            var sala = await _context.Set<Sala>().FindAsync(id);
            if (sala == null)
            {
                return NotFound($"No se encontró una sala con el ID {id}.");
            }

            try
            {
                _context.Set<Sala>().Remove(sala);
                await _context.SaveChangesAsync();

                return Ok($"Sala con ID {id} eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la sala: {ex.Message}");
            }
        }
    }
}


    