using Hotel_Umg_Frontend.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hotel_Umg_Frontend.Controllers
{
    public class UsuarioController : Controller
    {
        private string urlApi = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Usuario()
        {
            ViewBag.Message = "Usuarios";
            List<Usuario> usuarios = new List<Usuario>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync("/Api/usuario");
                    if (response.IsSuccessStatusCode)
                    {
                        usuarios = await response.Content.ReadAsAsync<List<Usuario>>();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Error en la solicitud al API. Código de estado: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException e)
                {
                    ModelState.AddModelError(string.Empty, $"Error al conectarse al API: {e.Message}");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, $"Ocurrió un error inesperado: {e.Message}");
                }
            }
            return View(usuarios);
        }

        // Crear Usuario
        [HttpPost]
        public async Task<ActionResult> Crear(Usuario nuevoUsuario)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync("/Api/usuario", nuevoUsuario);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Usuario");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el usuario");
                    return View(nuevoUsuario);
                }
            }
        }

        // Editar Usuario
        [HttpPost]
        public async Task<ActionResult> Editar(Usuario usuarioEditado)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PutAsJsonAsync($"/Api/usuario/{usuarioEditado.nombreUsuario}", usuarioEditado);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Usuario");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el usuario");
                    return View(usuarioEditado);
                }
            }
        }

        // Eliminar Usuario
        [HttpPost]
        public async Task<ActionResult> Eliminar(string NombreDeUsuario)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.DeleteAsync($"/Api/usuario/{NombreDeUsuario}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Usuario");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al eliminar el usuario");
                    return RedirectToAction("Usuario");
                }
            }
        }
    }
}
