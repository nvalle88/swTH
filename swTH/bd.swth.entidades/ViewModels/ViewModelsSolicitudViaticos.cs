using bd.swth.entidades.Negocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class ViewModelsSolicitudViaticos
    {
        public int IdSolicitudViatico { get; set; }
        public int IdPresupuesto { get; set; }
        public int IdEmpleado { get; set; }
        public string Servidor { get; set; }
        public int IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public int IdConfiguracionViatico { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Descripcion { get; set; }
        public decimal ValorEstimado { get; set; }
        public DateTime FechaLlegada { get; set; }
        public DateTime FechaSalida { get; set; }
        public string Observacion { get; set; }
        public int Estado { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraLlegada { get; set; }
        public int? IdEmpleadoAprobador { get; set; }
        public string FondoFinanciamiento { get; set; }
        public int IdFondoFinanciamiento { get; set; }
        public string Dependencia { get; set; }
        public string Puesto { get; set; }
        public List<TipoViatico> ListaTipoViatico { get; set; }
        public List<ViewModelsItinerarioViaticos> ListaItinerarioViatico { get; set; }
    }
}
