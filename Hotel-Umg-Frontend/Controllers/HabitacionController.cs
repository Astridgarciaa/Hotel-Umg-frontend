using Hotel_Umg_Frontend.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hotel_Umg_Frontend.Controllers
{

    public class HabitacionController : Controller
    {
        public class Respuesta
        {
            public List<Habitacion> habitaciones { get; set; }
            public List<Hotel> hoteles { get; set; }
            public List<TipoHabitacion> tiposhabitaciones { get; set; }

            public Respuesta(List<Habitacion> habitaciones, List<Hotel> hoteles, List<TipoHabitacion> tiposhabitaciones)
            {
                this.habitaciones=habitaciones;
                this.hoteles=hoteles;
                this.tiposhabitaciones = tiposhabitaciones;
            }
        }
        private string urlApi = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];

        // GET: Habitaciones
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Habitacion()
        {
            ViewBag.Message = "Habitaciones";
            List<Habitacion> habitaciones = new List<Habitacion>();
            List<Hotel> hoteles = new List<Hotel>();
            List<TipoHabitacion> tiposhabitaciones = new List<TipoHabitacion>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync("/Api/Habitacion");
                    if (response.IsSuccessStatusCode)
                    {
                        habitaciones = await response.Content.ReadAsAsync<List<Habitacion>>();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Error en la solicitud al API. Código de estado: {response.StatusCode}");
                    }

                    HttpResponseMessage responseHotel = await client.GetAsync("/Api/Hotel");
                    if (responseHotel.IsSuccessStatusCode)
                    {
                        hoteles = await responseHotel.Content.ReadAsAsync<List<Hotel>>();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Error en la solicitud al API. Código de estado: {response.StatusCode}");
                    }

                    HttpResponseMessage responseTipoHabitacion = await client.GetAsync("/Api/TipoHabitacion");
                    if (responseHotel.IsSuccessStatusCode)
                    {
                        tiposhabitaciones = await responseTipoHabitacion.Content.ReadAsAsync<List<TipoHabitacion>>();
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
            Respuesta respuesta = new Respuesta(habitaciones,hoteles,tiposhabitaciones);
            return View(respuesta);
        }

        // Crear Usuario
        [HttpPost]
        public async Task<ActionResult> Crear(Habitacion nuevaHabitacion)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync("/Api/Habitacion", nuevaHabitacion);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("habitacion");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el Habitacion");
                    return View(nuevaHabitacion);
                }
            }
        }

        // Editar Usuario
        [HttpPost]
        public async Task<ActionResult> Editar(Habitacion habitacionEditado)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PutAsJsonAsync($"/Api/Habitacion/{habitacionEditado.idHabitacion}", habitacionEditado);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Habitacion");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al editar la Habitación");
                    return View(habitacionEditado);
                }
            }
        }

        // Eliminar Usuario
        [HttpPost]
        public async Task<ActionResult> Eliminar(string idHabitacion)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.DeleteAsync($"/Api/Habitacion/{idHabitacion}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Habitacion");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al eliminar la Habitación");
                    return RedirectToAction("Habitacion");
                }
            }
        }
    }
}
