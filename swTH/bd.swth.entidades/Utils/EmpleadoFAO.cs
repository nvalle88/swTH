using bd.swth.entidades.Negocio;
using bd.swth.entidades.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.Utils
{
    public class EmpleadoFAO
    {
        public List<ActividadesAnalisisOcupacional> ListaActividad { get; set; }
        public DocumentoFAOViewModel Empleado { get; set; }

    }
}
