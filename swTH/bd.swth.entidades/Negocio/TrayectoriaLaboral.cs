namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TrayectoriaLaboral
    {
        [Key]
        public int IdTrayectoriaLaboral { get; set; }

        public int IdPersona { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public string Empresa { get; set; }
        public string TipoInstitucion { get; set; }
        public string FormaIngreso { get; set; }
        public string MotivoSalida { get; set; }
        public string AreaAsignada { get; set; }
        public string PuestoTrabajo { get; set; }

        public string DescripcionFunciones { get; set; }

        public virtual Persona Persona { get; set; }

        [NotMapped]
        [Display(Name = "Número de días")]
        public int NumeroDias { get; set; }

        [NotMapped]
        [Display(Name = "Años, meses y días")]
        public string TiempoTexto { get; set; }

    }
}
