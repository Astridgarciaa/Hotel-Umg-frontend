using Hotel_Umg_Frontend.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hotel_Umg_Frontend.Controllers
{

    public class DetalleController : Controller
    {
        public class Respuesta
        {
            public List<detalle> detalles { get; set; }
            public List<Reservacion> reservaciones { get; set; }
            public List<Habitacion> habitaciones { get; set; }

            public Respuesta(List<detalle> detalles, List<Reservacion> reservaciones, List<Habitacion> habitaciones)
            {
                this.detalles = detalles;
                this.reservaciones = reservaciones;
                this.habitaciones = habitaciones;
            }
        }
        private string urlApi = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];

        // GET: detalle
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> detalle()
        {
            ViewBag.Message = "detalles";
            List<detalle> detalles = new List<detalle>();
            List<Reservacion> reservaciones = new List<Reservacion>();
            List<Habitacion> habitaciones = new List<Habitacion>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync("/Api/detalle");
                    if (response.IsSuccessStatusCode)
                    {
                        detalles = await response.Content.ReadAsAsync<List<detalle>>();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Error en la solicitud al API. Código de estado: {response.StatusCode}");
                    }

                    HttpResponseMessage responseReservacion = await client.GetAsync("/Api/reservacion");
                    if (responseReservacion.IsSuccessStatusCode)
                    {
                        reservaciones = await responseReservacion.Content.ReadAsAsync<List<Reservacion>>();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Error en la solicitud al API. Código de estado: {response.StatusCode}");
                    }
                    HttpResponseMessage responseHabitacion = await client.GetAsync("/Api/habitacion");
                    if (responseHabitacion.IsSuccessStatusCode)
                    {
                        habitaciones = await responseHabitacion.Content.ReadAsAsync<List<Habitacion>>();
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
            Respuesta respuesta = new Respuesta(detalles, reservaciones, habitaciones);
            return View(respuesta);
        }

        // Crear detalle_reservacion
        [HttpPost]
        public async Task<ActionResult> Crear(detalle nuevoDetalle)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync("/Api/detalle", nuevoDetalle);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("detalle");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el detalle de la reservacion");
                    return View(nuevoDetalle);
                }
            }
        }

        // Editar detalle_reservacion
        [HttpPost]
        public async Task<ActionResult> Editar(detalle detalleEditado)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PutAsJsonAsync($"/Api/detalle/{detalleEditado.idDetalle}", detalleEditado);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("detalle");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el detalle de la reservacion");
                    return View(detalleEditado);
                }
            }
        }

        // Eliminar detalle_reservacion
        [HttpPost]
        public async Task<ActionResult> Eliminar(string idDetalle)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.DeleteAsync($"/Api/detalle/{idDetalle}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("detalle");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al eliminar el detalle de la reservacion");
                    return RedirectToAction("detalle");
                }
            }
        }
    }
}