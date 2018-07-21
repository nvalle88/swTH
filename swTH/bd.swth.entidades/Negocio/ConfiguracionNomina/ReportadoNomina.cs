using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace bd.swth.entidades.Negocio
{
   public class ReportadoNomina
    {
        [Key]
        public int IdReportadoNomina { get; set; }
        public string CodigoConcepto { get; set; }
        
        public string IdentificacionEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public double Cantidad { get; set; }
        public double Importe { get; set; }

        [NotMapped]
        public bool Valido { get; set; }
        [NotMapped]
        public string MensajeError { get; set; }

        [NotMapped]
        public string DescripcionConcepto { get; set; }

        public int IdCalculoNomina { get; set; }
        public virtual CalculoNomina CalculoNomina { get; set; }

       
    }
}
