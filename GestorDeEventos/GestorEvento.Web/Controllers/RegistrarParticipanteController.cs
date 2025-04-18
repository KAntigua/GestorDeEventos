using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using GestorEvento.Application.Interfaces;
using GestorEvento.Api.Servicios;
using GestorEvento.Web.Models;
using GestorEvento.Application.DTOs; // Asegúrate de tener la referencia a Newtonsoft.Json

namespace GestorEvento.Web.Controllers
{
    public class RegistrarParticipanteController : Controller
    {
        private readonly IRegistrarParticipanteService _registrarParticipanteService;
        private readonly IServicioEmail _servicioEmail;
        private readonly HttpClient _httpClient;
        public RegistrarParticipanteController(
            IRegistrarParticipanteService registrarParticipanteService,
            IServicioEmail servicioEmail,
            HttpClient httpClient)
        {
            _registrarParticipanteService = registrarParticipanteService;
            _servicioEmail = servicioEmail;
            _httpClient = httpClient;
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
                // Aquí construyes el DTO para enviar a la API
                var dto = new RegistrarParticipanteDTO
                {
                    Nombre = model.Nombre,
                    Correo = model.Correo,
                    EventoId = model.EventoId
                };

                // Serializa el DTO a JSON
                var jsonContent = JsonConvert.SerializeObject(dto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Enviar solicitud POST a la API para registrar al participante
                var response = await _httpClient.PostAsync("http://api_url/registrar", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Te has registrado exitosamente";
                    return RedirectToAction("Disponibles", "Sala");
                }
                else
                {
                    TempData["ErrorMessage"] = "Hubo un error al registrar el participante.";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error: " + ex.Message);
                return View(model);
            }
        }
    }
}

