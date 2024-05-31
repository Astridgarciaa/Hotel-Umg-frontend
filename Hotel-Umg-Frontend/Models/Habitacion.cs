using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel_Umg_Frontend.Models
{
    public class Habitacion
    {
        public int idHabitacion { get; set; }
        public int idHotel { get; set; }
        public virtual Hotel Hotel { get; set; }
      //  public int idTipoHabitacion { get; set; }
      //  public virtual TipoHabitacion TipoHabitacion { get; set; }
        public int disponibilidad { get; set; }

    }
}


