namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
     
     

    public partial class SolicitudPlanificacionVacaciones
    {
        [Key]
        public int IdSolicitudPlanificacionVacaciones { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int Estado { get; set; }
        public string Observaciones { get; set; }

        public virtual Empleado Empleado{ get; set; }
    }
}
