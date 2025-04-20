using GestorEvento.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;


namespace GestorEvento.Web.Controllers
{
    public class UsuarioLoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public UsuarioLoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44374/api");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Index(UsuarioLoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return View(loginDto);

            var json = JsonConvert.SerializeObject(loginDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Usuario/login", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Correo o clave incorrectos");
                return View(loginDto);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var usuario = JsonConvert.DeserializeObject<UsuarioDTO>(responseContent); 

            
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
            HttpContext.Session.SetString("UsuarioRol", usuario.Rol);

           
            if (usuario.Rol == "Administrador")
            {
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                return RedirectToAction("Disponibles", "Sala"); 
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }

}

