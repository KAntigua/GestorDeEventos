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
    public class ParticipanteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly GestorDbcontext _context;

        public ParticipanteController(IMapper mapper, GestorDbcontext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateParticipante([FromBody] ParticipanteDTO participanteDTO)
        {
            if (participanteDTO == null)
                return BadRequest("Los datos del participante no pueden ser nulos.");

            try
            {
                
                if (!participanteDTO.Correo.EndsWith("@gmail.com"))
                    return BadRequest("El correo debe tener el formato válido de @gmail.com.");

                var participante = _mapper.Map<Participante>(participanteDTO);

                await _context.Participantes.AddAsync(participante);
                await _context.SaveChangesAsync();

                var participanteCreadoDTO = _mapper.Map<ParticipanteDTO>(participante);
                return CreatedAtAction(nameof(GetParticipanteById), new { id = participante.Id }, participanteCreadoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el participante: {ex.Message}");
            }
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllParticipantes()
        {
            try
            {
                var participantes = await _context.Participantes.ToListAsync();

                if (participantes == null || !participantes.Any())
                {
                    return NotFound("No se encontraron participantes.");
                }

                var participantesDTO = _mapper.Map<List<ParticipanteDTO>>(participantes);
                return Ok(participantesDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los participantes: {ex.Message}");
            }
        }

      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipanteById(int id)
        {
            try
            {
                var participante = await _context.Participantes
                                                 .FirstOrDefaultAsync(p => p.Id == id);

                if (participante == null)
                    return NotFound($"No se encontró un participante con el ID {id}.");

                var participanteDTO = _mapper.Map<ParticipanteDTO>(participante);
                return Ok(participanteDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el participante: {ex.Message}");
            }
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParticipante(int id, [FromBody] ParticipanteDTO participanteDTO)
        {
            if (participanteDTO == null)
                return BadRequest("Los datos del participante no pueden ser nulos.");

            var participanteExistente = await _context.Participantes.FindAsync(id);
            if (participanteExistente == null)
            {
                return NotFound($"No se encontró un participante con el ID {id}.");
            }

            try
            {
               
                if (!participanteDTO.Correo.EndsWith("@gmail.com"))
                    return BadRequest("El correo debe tener el formato válido de @gmail.com.");

               
                participanteExistente.Nombre = participanteDTO.Nombre;
                participanteExistente.Correo = participanteDTO.Correo;

                await _context.SaveChangesAsync();

                var participanteActualizadoDTO = _mapper.Map<ParticipanteDTO>(participanteExistente);
                return Ok(participanteActualizadoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el participante: {ex.Message}");
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipante(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
            {
                return NotFound($"No se encontró un participante con el ID {id}.");
            }

            try
            {
                _context.Participantes.Remove(participante);
                await _context.SaveChangesAsync();

                return Ok($"Participante con ID {id} eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el participante: {ex.Message}");
            }
        }
    }
}
