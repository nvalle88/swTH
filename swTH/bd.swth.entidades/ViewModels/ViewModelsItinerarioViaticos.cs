using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class ViewModelsItinerarioViaticos
    {
        public int IdItinerarioViatico { get; set; }
        public int IdSolicitudViatico { get; set; }
        public int IdTipoTransporte { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraLlegada { get; set; }
        public int IdCiudadOrigen { get; set; }
        public int IdCiudadDestino { get; set; }
        public string Ruta { get; set; }
        public string Transporte { get; set; }
    }
}
