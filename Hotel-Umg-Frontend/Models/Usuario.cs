using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel_Umg_Frontend.Models
{
    public class Usuario
    {
        
        public int idUsuario { get; set; }

        public string nombreUsuario { get; set; }

        public string password { get; set; }

        public int idEmpleado { get; set; }
    }
}