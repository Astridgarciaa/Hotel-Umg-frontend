using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel_Umg_Frontend.Models
{
    public class Empleado
    {
        public int idEmpleado { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cargo { get; set; }
        public decimal salario { get; set; }
        public DateTime fechaContratacion { get; set; }

    }
}


