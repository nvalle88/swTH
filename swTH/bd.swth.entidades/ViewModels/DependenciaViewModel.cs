using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class DependenciaViewModel
    {
        public int IdDependencia { get; set; }
        public int IdSucursal { get; set; }
        public int IdProceso { get; set; }
        public int IdCiudad { get; set; }
        public int IdDependenciaPadre { get; set; }
        public string NombreDependencia { get; set; }
        public string NombreSucursal { get; set; }
        public string NombreDependenciaPadre { get; set; }
        public string NombreProceso { get; set; }
    }
}
