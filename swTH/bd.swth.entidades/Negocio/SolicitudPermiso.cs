namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
     
    public partial class SolicitudPermiso
    {
        [Key]
        public int IdSolicitudPermiso { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public TimeSpan? HoraDesde { get; set; }
        public TimeSpan? HoraHasta { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string Motivo { get; set; }
        public string Observacion { get; set; }
        public int? Estado { get; set; }
        public int? IdTipoPermiso { get; set; }
        public DateTime? FechaAprobado { get; set; }
        public bool CargoVacaciones { get; set; }

        public virtual Empleado Empleado { get; set; }
        public virtual TipoPermiso TipoPermiso { get; set; }
    }
}
