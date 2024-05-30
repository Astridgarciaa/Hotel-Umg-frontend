using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel_Umg_Frontend.Models
{
    public class Hotel
    {
        public int idDetalleReservacion { get; set; }
        public DateTime fechaReserva { get; set; }
    }
}