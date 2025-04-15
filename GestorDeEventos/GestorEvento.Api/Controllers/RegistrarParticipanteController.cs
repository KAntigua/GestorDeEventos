using GestorEvento.Api.Servicios;
using GestorEvento.Application.DTOs;
using GestorEvento.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace GestorEvento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrarParticipanteController : ControllerBase
    {
        private readonly RegistrarParticipanteService _registrarParticipanteService;
        private readonly IServicioEmail _servicioEmail;

        public RegistrarParticipanteController(
            RegistrarParticipanteService registrarParticipanteService,
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
                var idParticipante = await _registrarParticipanteService.CreateAsync(dto);

                await _servicioEmail.EnviarEmail(
                    dto.Correo,
                    "Confirmación de Registro al Evento",
                    $"Hola {dto.Nombre},\n\nTe has registrado exitosamente en el evento con ID {dto.EventoId}.\n\n¡Gracias por participar!"
                );

                return Ok($"Te has registrado exitosamente. ID del participante: {idParticipante}");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
            }
        }
    }
}