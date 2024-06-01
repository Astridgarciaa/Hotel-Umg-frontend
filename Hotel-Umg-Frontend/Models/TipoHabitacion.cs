using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel_Umg_Frontend.Models
{
    public class TipoHabitacion
    {
        public int idTipoHabitacion { get; set; }
        public string nombreTipo { get; set; }
        public string descripcion { get; set; }

    }
}


