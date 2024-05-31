using Hotel_Umg_Frontend.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hotel_Umg_Frontend.Controllers
{

    public class ClienteController : Controller
    {
        public class Respuesta
        {
            public List<Cliente> clientes { get; set; }

            public Respuesta(List<Cliente> clientes)
            {
                this.clientes=clientes;
            }
        }
        private string urlApi = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];

        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Cliente()
        {
            ViewBag.Message = "Clientes";
            List<Cliente> clientes = new List<Cliente>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync("/Api/cliente");
                    if (response.IsSuccessStatusCode)
                    {
                        clientes = await response.Content.ReadAsAsync<List<Cliente>>();
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
            Respuesta respuesta = new Respuesta(clientes);
            return View(respuesta);
        }

        // Crear Usuario
        [HttpPost]
        public async Task<ActionResult> Crear(Cliente nuevoCliente)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync("/Api/cliente", nuevoCliente);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Cliente");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear al cliente");
                    return View(nuevoCliente);
                }
            }
        }

        // Editar Cliente
        [HttpPost]
        public async Task<ActionResult> Editar(Cliente clienteEditado)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PutAsJsonAsync($"/Api/cliente/{clienteEditado.idCliente}", clienteEditado);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Cliente");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el cliente");
                    return View(clienteEditado);
                }
            }
        }

        // Eliminar Cliente
        [HttpPost]
        public async Task<ActionResult> Eliminar(string idCliente)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.DeleteAsync($"/Api/cliente/{idCliente}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Cliente");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al eliminar el cliente");
                    return RedirectToAction("Cliente");
                }
            }
        }
    }
}
