namespace bd.swth.entidades.Negocio
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class AccionPersonal
    {
        [Key]
        public int IdAccionPersonal { get; set; }

        public DateTime Fecha { get; set; }

      
        public string Numero { get; set; }

    
        public string Solicitud { get; set; }

      
        public string Explicacion { get; set; }

        
        public DateTime FechaRige { get; set; }

      
        public DateTime FechaRigeHasta { get; set; }


        public int NoDias { get; set; }

        public int Estado { get; set; }

        //Referencias a tablas

        public int IdEmpleado { get; set; }

        public int IdTipoAccionPersonal { get; set; }

        //Propiedades Virtuales
        public virtual Empleado Empleado { get; set; }

        public virtual TipoAccionPersonal TipoAccionPersonal { get; set; }
    }
}
