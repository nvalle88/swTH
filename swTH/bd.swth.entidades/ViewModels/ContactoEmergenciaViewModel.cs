using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class ContactoEmergenciaViewModel
    {
        public int IdPersona { get; set; }
        public int IdEmpleadoContactoEmergencia{ get; set; }
        public int IdEmpleado { get; set; }
        public int IdParentesco { get; set; }
        public string Parentesco { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TelefonoPrivado { get; set; }
        public string TelefonoCasa { get; set; }
    }
}
