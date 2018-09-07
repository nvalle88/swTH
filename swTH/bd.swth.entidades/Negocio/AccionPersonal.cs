namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AccionPersonal
    {

        public int IdAccionPersonal { get; set; }
        public int IdEmpleadoAfectado { get; set; }
        public int IdEmpleadoResponsableIngreso { get; set; }
        public int IdTipoAccionPersonal { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Solicitud { get; set; }
        public string Explicacion { get; set; }
        public DateTime FechaRige { get; set; }
        public DateTime? FechaRigeHasta { get; set; }
        public int TotalDias { get; set; }
        public int? DiasRestantes { get; set; }
        public int Estado { get; set; }
        public bool Bloquear { get; set; }
        public bool Ejecutado { get; set; }

        public virtual ICollection<AprobacionAccionPersonal> AprobacionAccionPersonal { get; set; }
        public virtual ICollection<EmpleadoMovimiento> EmpleadoMovimiento { get; set; }
        public virtual Empleado EmpleadoAfectado { get; set; }
        public virtual Empleado EmpleadoResponsableIngreso { get; set; }
        public virtual TipoAccionPersonal TipoAccionPersonal { get; set; }


        [NotMapped]
        public string NombreUsuario { get; set; }

        
    }
}
