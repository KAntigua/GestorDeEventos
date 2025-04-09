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

    public class EventoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly GestorDbcontext _context;

        public EventoController(IMapper mapper, GestorDbcontext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvento([FromBody] EventoDTO eventoDTO)
        {
            if (eventoDTO == null)
                return BadRequest("Los datos del evento no pueden ser nulos.");

            try
            {
                var evento = _mapper.Map<Evento>(eventoDTO);

                await _context.Eventos.AddAsync(evento);
                await _context.SaveChangesAsync();

                var eventoCreadoDTO = _mapper.Map<EventoDTO>(evento);
                return CreatedAtAction(nameof(GetEventoById), new { id = evento.Id }, eventoCreadoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el evento: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventos()
        {
            try
            {
                var eventos = await _context.Eventos.ToListAsync();
                var eventosDTO = _mapper.Map<List<EventoDTO>>(eventos);
                return Ok(eventosDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los eventos: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoById(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
                return NotFound($"No se encontró un evento con el ID {id}.");

            var eventoDTO = _mapper.Map<EventoDTO>(evento);
            return Ok(eventoDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvento(int id, [FromBody] EventoDTO eventoDTO)
        {
            var eventoExistente = await _context.Eventos.FindAsync(id);
            if (eventoExistente == null)
                return NotFound($"No se encontró un evento con el ID {id}.");

            try
            {
                _mapper.Map(eventoDTO, eventoExistente);
                await _context.SaveChangesAsync();

                var eventoActualizadoDTO = _mapper.Map<EventoDTO>(eventoExistente);
                return Ok(eventoActualizadoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el evento: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
                return NotFound($"No se encontró un evento con el ID {id}.");

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();

            return Ok($"Evento con ID {id} eliminado correctamente.");
        }
    }
}

 