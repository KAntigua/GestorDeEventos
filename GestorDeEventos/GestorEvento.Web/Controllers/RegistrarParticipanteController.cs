using GestorEvento.Api.Servicios;
using GestorEvento.Application.DTOs;
using GestorEvento.Application.Interfaces;
using GestorEvento.Application.Services;
using GestorEvento.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestorEvento.Web.Controllers
{
    public class RegistrarParticipanteController : Controller
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

        [HttpGet]
        public IActionResult Registrar(int eventoId)
        {
            var viewModel = new RegistrarParticipanteViewModel
            {
                EventoId = eventoId
            };

            return View(viewModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegistrarParticipanteViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var dto = new RegistrarParticipanteDTO
                {
                    Nombre = model.Nombre,
                    Correo = model.Correo,
                    EventoId = model.EventoId
                };

                var idParticipante = await _registrarParticipanteService.RegistrarParticipanteAsync(dto);

                await _servicioEmail.EnviarEmail(
                    model.Correo,
                    "Confirmación de Registro al Evento",
                    $"Hola {model.Nombre},\n\nTe has registrado exitosamente en el evento con ID {model.EventoId}.\n\n¡Gracias por participar!"
                );

                TempData["SuccessMessage"] = "Te has registrado exitosamente";
                return RedirectToAction("Disponibles", "Sala");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al registrar al participante.");
            }



            return RedirectToAction("Disponibles", "Sala");
        }
    }
}

