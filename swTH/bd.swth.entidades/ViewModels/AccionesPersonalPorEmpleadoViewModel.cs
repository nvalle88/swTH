using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class AccionesPersonalPorEmpleadoViewModel
    {

        public string NombreUsuarioActual { get; set; }

        public List<AccionPersonalViewModel> ListaAccionPersonal { get; set; }
        
        public DatosBasicosEmpleadoViewModel DatosBasicosEmpleadoViewModel { get; set; }


    }
}
