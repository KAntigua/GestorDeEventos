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

    public class EventoParticipanteController : Controller
    {
        private readonly IMapper _mapper;
        private readonly GestorDbcontext _context;

        public EventoParticipanteController(IMapper mapper, GestorDbcontext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventoParticipante([FromBody] EventoParticipanteDTO eventoParticipanteDTO)
        {
            if (eventoParticipanteDTO == null)
                return BadRequest("Los datos del evento-participante no pueden ser nulos.");

            try
            {
                
                var evento = await _context.Eventos.FindAsync(eventoParticipanteDTO.EventoId);
                if (evento == null)
                {
                    return NotFound($"El evento con ID {eventoParticipanteDTO.EventoId} no existe.");
                }

              
                var participante = await _context.Participantes.FindAsync(eventoParticipanteDTO.ParticipanteId);
                if (participante == null)
                {
                    return NotFound($"El participante con ID {eventoParticipanteDTO.ParticipanteId} no existe.");
                }

              
                var eventoParticipante = _mapper.Map<EventoParticipante>(eventoParticipanteDTO);

                
                await _context.EventoParticipantes.AddAsync(eventoParticipante);
                await _context.SaveChangesAsync();

               
                var eventoParticipanteCreadoDTO = _mapper.Map<EventoParticipanteDTO>(eventoParticipante);

                
                return CreatedAtAction(nameof(GetEventoParticipanteById), new { id = eventoParticipante.Id }, eventoParticipanteCreadoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la relación evento-participante: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoParticipanteById(int id)
        {
            var eventoParticipante = await _context.EventoParticipantes
                .Include(ep => ep.Evento)   
                .Include(ep => ep.Participante) 
                .FirstOrDefaultAsync(ep => ep.Id == id);

            if (eventoParticipante == null)
                return NotFound($"La relación con ID {id} no existe.");

            var eventoParticipanteDTO = _mapper.Map<EventoParticipanteDTO>(eventoParticipante);
            return Ok(eventoParticipanteDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventoParticipantes()
        {
            var eventoParticipantes = await _context.EventoParticipantes
                .Include(ep => ep.Evento) 
                .Include(ep => ep.Participante)  
                .ToListAsync();

            if (eventoParticipantes == null || eventoParticipantes.Count == 0)
                return NotFound("No hay relaciones de evento-participante.");

            var eventoParticipantesDTO = _mapper.Map<List<EventoParticipanteDTO>>(eventoParticipantes);
            return Ok(eventoParticipantesDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEventoParticipante(int id, [FromBody] EventoParticipanteDTO eventoParticipanteDTO)
        {
            if (eventoParticipanteDTO == null)
                return BadRequest("Los datos del evento-participante no pueden ser nulos.");

            var eventoParticipante = await _context.EventoParticipantes
                .FirstOrDefaultAsync(ep => ep.Id == id);

            if (eventoParticipante == null)
                return NotFound($"La relación con ID {id} no existe.");

            
            eventoParticipante.FechaRegistro = eventoParticipanteDTO.FechaRegistro;

         
            _context.EventoParticipantes.Update(eventoParticipante);
            await _context.SaveChangesAsync();

            var eventoParticipanteActualizadoDTO = _mapper.Map<EventoParticipanteDTO>(eventoParticipante);
            return Ok(eventoParticipanteActualizadoDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventoParticipante(int id)
        {
            var eventoParticipante = await _context.EventoParticipantes
                .FirstOrDefaultAsync(ep => ep.Id == id);

            if (eventoParticipante == null)
            {
                return NotFound($"No se encontró la relación EventoParticipante con el ID {id}.");
            }

            try
            {
                _context.EventoParticipantes.Remove(eventoParticipante);
                await _context.SaveChangesAsync();

                return Ok($"Relación EventoParticipante con ID {id} eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la relación: {ex.Message}");
            }
        }

    }
}
