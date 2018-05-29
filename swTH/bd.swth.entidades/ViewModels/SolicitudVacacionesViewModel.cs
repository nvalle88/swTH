using bd.swth.entidades.Negocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class SolicitudVacacionesViewModel
    {
        public DatosBasicosEmpleadoViewModel DatosBasicosEmpleadoViewModel { get; set; }

        public int IdSolicitudVacaciones { get; set; }
        
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }

        public DateTime FechaRespuesta { get; set; }

        public bool PlanAnual { get; set; }

        public int Estado { get; set; }
        public string NombreEstado { get; set; }


        public string Observaciones { get; set; }

        public int VacacionesAcumuladas { get; set; }

        public List<SolicitudPlanificacionVacaciones> ListaPLanificacionVacaciones { get; set; }

        public int IdSolicitudPlanificacionVacaciones { get; set; }

    }
}
