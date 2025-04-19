using GestorEvento.Application.DTOs;
using GestorEvento.Application.Services;
using GestorEvento.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace GestorEvento.Web.Controllers
{
    public class EventoController : Controller
    {
        private readonly HttpClient _httpClient;

        public EventoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44374/api");
        }

       
 
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Evento");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var eventos = JsonConvert.DeserializeObject<IEnumerable<EventoViewModel>>(content);
                return View("Index", eventos);
            }

            return View(new List<EventoViewModel>());

        }

        public IActionResult Create(int? salaId = null)
        {
            var model = new EventoDTO();

            if (salaId.HasValue)
            {
                model.SalaId = salaId.Value;
            }

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Create(EventoDTO eventos)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(eventos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Evento", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(responseData);

                    int nuevoEventoId = result.id;

                    
                    return RedirectToAction("Details", new { id = nuevoEventoId });
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el evento.");
                }
            }

            return View(eventos);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Evento/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var eventos = JsonConvert.DeserializeObject<EventoViewModel>(content);

                return View(eventos);

            }
            else
            {
                return RedirectToAction("Details");


            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EventoDTO eventos)
        {


            var json = JsonConvert.SerializeObject(eventos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/Evento/{id}", content);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", new { id });

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar el evento.");
            }

            return View(eventos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/Evento/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var eventos = JsonConvert.DeserializeObject<EventoViewModel>(content);

                return View(eventos);
            }
            else
            {
                return RedirectToAction("Details");
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Evento/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar el evento.";
                return RedirectToAction("Index");
            }
        }

       

    }
}
