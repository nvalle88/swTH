using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class SolicitudPlanificacionVacacionesViewModel
    {
        public DatosBasicosEmpleadoViewModel DatosBasicosEmpleadoViewModel { get; set; }

        public int IdSolicitudPlanificacionVacaciones { get; set; }
        
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        
        public int Estado { get; set; }
        public string NombreEstado { get; set; }


        public string Observaciones { get; set; }

        public int VacacionesAcumuladas { get; set; }
        
    }
}
