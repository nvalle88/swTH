namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;



    public partial class SolicitudVacaciones
    {
        [Key]
        public int IdSolicitudVacaciones { get; set; }

       
        public DateTime FechaSolicitud { get; set; }

        
        public DateTime FechaDesde { get; set; }

       
        public DateTime FechaHasta { get; set; }

     
        public DateTime? FechaRespuesta { get; set; }

      
        public string Observaciones { get; set; }

        public int Estado { get; set; }

      
        public bool PlanAnual { get; set; }

  
        public int IdEmpleado { get; set; }
        public virtual Empleado Empleado { get; set; }

    }
}
