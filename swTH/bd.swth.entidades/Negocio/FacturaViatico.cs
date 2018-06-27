namespace bd.swth.entidades.Negocio
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class FacturaViatico
    {
        [Key]
        public int IdFacturaViatico { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public decimal ValorTotalFactura { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime? ValorTotalAprobacion { get; set; }
        public string Observaciones { get; set; }
        public int IdItemViatico { get; set; }
        public int IdSolicitudViatico { get; set; }
        public int AprobadoPor { get; set; }
        public string Url { get; set; }

        public virtual ItemViatico ItemViatico { get; set; }
        public virtual SolicitudViatico SolicitudViatico { get; set; }
    }
}
