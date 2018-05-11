using bd.swth.entidades.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class ViewModelFacturaViatico
    {

        [Key]
        public int IdFacturaViatico { get; set; }
        public int IdSolicitudViatico { get; set; }

        public string NumeroFactura { get; set; }

        public decimal ValorTotalFactura { get; set; }

        public DateTime FechaFactura { get; set; }

        public DateTime? FechaAprobacion { get; set; }

        public decimal? ValorTotalAprobacion { get; set; }

        public string Observaciones { get; set; }

        //Propiedades Virtuales Referencias a otras clases

        public int IdItemViatico { get; set; }
        //Propiedades Virtuales Referencias a otras clases

     
        public int IdItinerarioViatico { get; set; }

        //Propiedades Virtuales Referencias a otras clases

    
        public int AprobadoPor { get; set; }
        public string Url { get; set; }

        public byte[] Fichero { get; set; }

    }
}
