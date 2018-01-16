namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
     
     

    public partial class SolicitudHorasExtras
    {
        [Key]
        public long IdSolicitudHorasExtras { get; set; }

       
        public DateTime Fecha { get; set; }

        public DateTime? FechaSolicitud { get; set; }

       
        public DateTime? FechaAprobado { get; set; }

      
        public int? CantidadHoras { get; set; }

       
        public string Observaciones { get; set; }

       
        public int? Estado { get; set; }

       
        public bool EsExtraordinaria { get; set; }

        //Propiedades Virtuales Referencias a otras clases

       
        public int IdEmpleado { get; set; }
        public virtual Empleado Empleado { get; set; }

    }
}
