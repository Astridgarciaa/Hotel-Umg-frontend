using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel_Umg_Frontend.Models
{
    public class detalle
    {

        public int idDetalle { get; set; }

        public int idreservacion { get; set; }

        public int idhabitacion { get; set; }

        public DateTime fechaReserva { get; set; }


        public int idReservacion { get; set; } 
        public virtual Reservacion Reservacion { get; set; }


        public int idHabitacion { get; set;  }
        public virtual Habitacion Habitacion { get; set; }
    }
}