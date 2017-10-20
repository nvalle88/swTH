using bd.swth.entidades.Negocio;
using System.Collections.Generic;

namespace bd.swth.entidades.ObjectTransfer
{
    public class EmpleadoViewModel
    {
        public Persona Persona { get; set; }
        public List<EmpleadoFamiliar> EmpleadoFamiliar { get; set; }
        public Empleado Empleado { get; set; }
        public DatosBancarios DatosBancarios { get; set; }
    }

    
}
