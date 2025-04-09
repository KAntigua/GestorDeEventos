using GestorEvento.Api.Servicios;
using GestorEvento.Application.DTOs;
using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorEvento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrarParticipanteController : ControllerBase
    {
        private readonly GestorDbcontext context;
        private readonly IServicioEmail servicioEmail;

        public RegistrarParticipanteController(GestorDbcontext context, IServicioEmail servicioEmail)
        {
            this.context = context;
            this.servicioEmail = servicioEmail;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarEnEvento([FromBody] RegistrarParticipanteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            
            var participante = await context.Participantes
                .FirstOrDefaultAsync(p => p.Correo == dto.Correo);

            if (participante == null)
            {
                participante = new Participante
                {
                    Nombre = dto.Nombre,
                    Correo = dto.Correo
                };
                context.Participantes.Add(participante);
                await context.SaveChangesAsync();
            }

            var yaRegistrado = await context.EventoParticipantes
                .AnyAsync(ep => ep.EventoId == dto.EventoId && ep.ParticipanteId == participante.Id);

            if (yaRegistrado)
                return BadRequest("Ya estás registrado en este evento");

            var eventoParticipante = new EventoParticipante
            {
                EventoId = dto.EventoId,
                ParticipanteId = participante.Id,
                FechaRegistro = DateTime.Now
            };

            context.EventoParticipantes.Add(eventoParticipante);
            await context.SaveChangesAsync();

            await servicioEmail.EnviarEmail(
            dto.Correo,
            "Confirmación de Registro al Evento",
            $"Hola {dto.Nombre},\n\nTe has registrado exitosamente en el evento con ID {dto.EventoId}.\n\n¡Gracias por participar!"
            );


            return Ok("Te has registrado exitosamente");
        }
    }
}
