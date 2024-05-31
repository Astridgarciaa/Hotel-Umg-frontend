using Hotel_Umg_Frontend.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hotel_Umg_Frontend.Controllers
{
    public class HotelController : Controller
    {
        private string urlApi = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];

        // GET: Hotel
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Hotel()
        {
            ViewBag.Message = "Hotel";
            List<Hotel> hotel = new List<Hotel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync("/Api/hotel");
                    if (response.IsSuccessStatusCode)
                    {
                        hotel = await response.Content.ReadAsAsync<List<Hotel>>();
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
            return View(hotel);
        }

        // Crear  Hotel
        [HttpPost]
        public async Task<ActionResult> Crear(Hotel nuevoHotel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync("/Api/hotel", nuevoHotel);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Hotel");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el hotel");
                    return View(nuevoHotel);
                }
            }
        }

        // Editar Hotel
        [HttpPost]
        public async Task<ActionResult> Editar(Hotel hotelEditado)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PutAsJsonAsync($"/Api/hotel/{hotelEditado.idHotel}", hotelEditado);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Hotel");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el hotel");
                    return View(hotelEditado);
                }
            }
        }

        // Eliminar Hotel
        [HttpPost]
        public async Task<ActionResult> Eliminar(string idHotel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.DeleteAsync($"/Api/hotel/{idHotel}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Hotel");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al eliminar el hotel");
                    return RedirectToAction("Hotel");
                }
            }
        }
    }
}
