using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel_Umg_Frontend.Models
{
    public class Reservacion
    {
        public int idReservacion { get; set; }
        public int idCliente { get; set; }
        public int idEmpleado { get; set; }
        public int idHotel { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public float costoFinal { get; set; }

    }
}


