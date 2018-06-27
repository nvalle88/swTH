
using bd.swth.entidades.Negocio;
using bd.swth.entidades.ObjectTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class SolicitudViaticoViewModel
    {
        public SolicitudViatico SolicitudViatico { get; set; }
        public Presupuesto Presupuesto { get; set; }
        public List<SolicitudTipoViatico> SolicitudTipoViatico { get; set; }
        public List<ItinerarioViatico> ItinerarioViatico { get; set; }
        public ListaEmpleadoViewModel ListaEmpleadoViewModel { get; set; }
        public List<TipoViatico> ListaTipoViatico { get; set; }
        public List<String> ViaticosSeleccionados { get; set; }
        public Decimal Valor { get; set; }
        
    }
}
