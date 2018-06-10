namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AccionPersonal
    {

        [Key]
        public int IdAccionPersonal { get; set; }
        public int IdEmpleado { get; set; }
        public int IdTipoAccionPersonal { get; set; }
        public DateTime Fecha { get; set; }
        public string Numero { get; set; }
        public string Solicitud { get; set; }
        public string Explicacion { get; set; }
        public DateTime FechaRige { get; set; }
        public DateTime? FechaRigeHasta { get; set; }
        public int? NoDias { get; set; }
        public int Estado { get; set; }
        public bool Bloquear { get; set; }
        public bool Ejecutado { get; set; }

        [NotMapped]
        public string NombreUsuario { get; set; }

        public virtual ICollection<EmpleadoMovimiento> EmpleadoMovimiento { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual TipoAccionPersonal TipoAccionPersonal{ get; set; }
        public virtual ICollection<AprobacionAccionPersonal> AprobacionAccionPersonal { get; set; }

        
        
    }
}
