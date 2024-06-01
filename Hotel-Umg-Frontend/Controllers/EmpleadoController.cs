using Hotel_Umg_Frontend.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hotel_Umg_Frontend.Controllers
{

    public class EmpleadoController : Controller
    {
        public class Respuesta
        {
            public List<Empleado> empleados { get; set; }
            public List<Hotel> hoteles { get; set; }

            public Respuesta(List<Empleado> empleados, List<Hotel> hoteles)
            {
                this.empleados=empleados;
                this.hoteles=hoteles;
            }
        }
        private string urlApi = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];

        // GET: Empleado
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Empleado()
        {
            ViewBag.Message = "Empleados";
            List<Empleado> empleados = new List<Empleado>();
            List<Hotel> hoteles = new List<Hotel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync("/Api/empleado");
                    if (response.IsSuccessStatusCode)
                    {
                        empleados = await response.Content.ReadAsAsync<List<Empleado>>();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Error en la solicitud al API. Código de estado: {response.StatusCode}");
                    }

                    HttpResponseMessage responseHoteles = await client.GetAsync("/Api/hotel");
                    if (responseHoteles.IsSuccessStatusCode)
                    {
                        hoteles = await responseHoteles.Content.ReadAsAsync<List<Hotel>>();
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
            Respuesta respuesta = new Respuesta(empleados, hoteles);
            return View(respuesta);
        }

        // Crear Empleado
        [HttpPost]
        public async Task<ActionResult> Crear(Empleado nuevoEmpleado)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync("/Api/empleado", nuevoEmpleado);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Empleado");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el empleado");
                    return View(nuevoEmpleado);
                }
            }
        }

        // Editar Empleado
        [HttpPost]
        public async Task<ActionResult> Editar(Empleado empleadoEditado)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PutAsJsonAsync($"/Api/empleado/{empleadoEditado.idEmpleado}", empleadoEditado);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Empleado");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el empleado");
                    return View(empleadoEditado);
                }
            }
        }

        // Eliminar Empleado
        [HttpPost]
        public async Task<ActionResult> Eliminar(string idEmpleado)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.DeleteAsync($"/Api/empleado/{idEmpleado}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Empleado");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al eliminar el Empleado");
                    return RedirectToAction("Empleado");
                }
            }
        }
    }
}
