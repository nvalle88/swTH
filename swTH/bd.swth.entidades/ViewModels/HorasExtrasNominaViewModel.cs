using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
   public class HorasExtrasNominaViewModel
    {
       
        public string NombreApellido { get; set; }
        public int CantidadHoras { get; set; }
        public string IdentificacionEmpleado { get; set; }
        public bool EsExtraordinaria { get; set; }
    }
}
