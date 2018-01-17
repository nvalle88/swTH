namespace bd.swth.entidades.Negocio
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SolicitudCertificadoPersonal
    {
        [Key]
        public int IdSolicitudCertificadoPersonal { get; set; }
        public bool IdEstado { get; set; }
        public int IdTipoCertificado { get; set; }
        public int IdEmpleadoSolicitante { get; set; }
        public int IdEmpleadoDirigidoA { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Observaciones { get; set; }

        public virtual Empleado IdEmpleadoDirigidoANavigation { get; set; }
        public virtual Empleado IdEmpleadoSolicitanteNavigation { get; set; }
        public virtual TipoCertificado IdTipoCertificadoNavigation { get; set; }
    }
}
