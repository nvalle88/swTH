namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SolicitudViatico
    {
        [Key]

        public int IdSolicitudViatico { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCiudad { get; set; }
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
        public int IdFondoFinanciamiento { get; set; }

        public virtual ICollection<AprobacionViatico> AprobacionViatico { get; set; }
        public virtual ICollection<InformeActividadViatico> InformeActividadViatico { get; set; }
        public virtual ICollection<FacturaViatico> FacturaViatico { get; set; }
        public virtual ICollection<DetallePresupuesto> DetallePresupuesto { get; set; }
        public virtual ICollection<InformeViatico> InformeViatico { get; set; }
        public virtual ICollection<ItinerarioViatico> ItinerarioViatico { get; set; }
        public virtual ICollection<SolicitudTipoViatico> SolicitudTipoViatico { get; set; }
        public virtual ICollection<ReliquidacionViatico> ReliquidacionViatico { get; set; }
        public virtual Ciudad Ciudad { get; set; }
        public virtual ConfiguracionViatico ConfiguracionViatico { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual FondoFinanciamiento FondoFinanciamiento { get; set; }
        

    }
}
