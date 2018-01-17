using bd.swth.entidades.Negocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class SolicitudPermisoViewModel
    {
        public string NombresApellidosEmpleado { get; set; }
        public string NombreDependencia { get; set; }
        public SolicitudPermiso SolicitudPermiso { get; set; }
        public int IdTipoPermiso { get; set; }
    }
}
