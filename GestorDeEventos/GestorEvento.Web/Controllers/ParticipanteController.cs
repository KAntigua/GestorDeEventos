using GestorEvento.Application.DTOs;
using GestorEvento.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GestorEvento.Web.Controllers
{
    public class ParticipanteController : Controller
    {
        private readonly HttpClient _httpClient;

        public ParticipanteController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44374/api");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Participante");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var participantes = JsonConvert.DeserializeObject<IEnumerable<ParticipanteViewModel>>(content);
                return View("Index", participantes);
            }

            return View(new List<ParticipanteViewModel>());

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(ParticipanteDTO participantes)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(participantes);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Participante", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el Participante.");
                }
            }

            return View(participantes);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Participante/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var participantes = JsonConvert.DeserializeObject<ParticipanteViewModel>(content);

                return View(participantes);

            }
            else
            {
                return RedirectToAction("Details");


            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ParticipanteViewModel participantes)
        {


            var json = JsonConvert.SerializeObject(participantes);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/Participante/{id}", content);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", new { id });

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar el participante.");
            }

            return View(participantes);
        }





        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/Participante/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var participantes = JsonConvert.DeserializeObject<ParticipanteViewModel>(content);

                return View(participantes);
            }
            else
            {
                return RedirectToAction("Details");
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Participante/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar el participante.";
                return RedirectToAction("Index");
            }
        }
    }
}

