using GestorEvento.Api.Servicios;
using GestorEvento.Application.DTOs;
using GestorEvento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestorEvento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrarParticipanteController : ControllerBase
    {
        private readonly IRegistrarParticipanteService _registrarParticipanteService;
        private readonly IServicioEmail _servicioEmail;

        public RegistrarParticipanteController(
            IRegistrarParticipanteService registrarParticipanteService,
            IServicioEmail servicioEmail)
        {
            _registrarParticipanteService = registrarParticipanteService;
            _servicioEmail = servicioEmail;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarEnEvento([FromBody] RegistrarParticipanteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var idParticipante = await _registrarParticipanteService.RegistrarParticipanteAsync(dto);

                await _servicioEmail.EnviarEmail(
                    dto.Correo,
                    "Confirmación de Registro al Evento",
                    $"Hola {dto.Nombre},\n\nTe has registrado exitosamente en el evento con ID {dto.EventoId}.\n\n¡Gracias por participar!"
                );

                return Ok(new
                {
                    mensaje = "Te has registrado exitosamente.",
                    participanteId = idParticipante
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
            }
        }
    }
}
