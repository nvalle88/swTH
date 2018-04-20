using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class LavadoActivoEmpleadoViewModel
    {
        public string NombreUsuario { get; set; }

        public DatosBasicosEmpleadoViewModel DatosBasicosEmpleadoViewModel { get; set; }

        public List<LavadoActivoItemViewModel> ListaLavadoActivoItemViewModel { get; set; }
    }
}
