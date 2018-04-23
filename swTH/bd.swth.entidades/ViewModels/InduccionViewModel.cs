using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class InduccionViewModel
    {
        public int IdEmpleado { get; set; }
        public string Nombres { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string EstadoInduccion { get; set; }
        public string ValorCompletado { get; set; }
    }
}
