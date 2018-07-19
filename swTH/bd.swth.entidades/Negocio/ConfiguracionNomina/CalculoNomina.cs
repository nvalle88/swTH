using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.Negocio
{
   public class CalculoNomina
    {
        [Key]
        public int IdCalculoNomina { get; set; }
        public bool EmpleadoActivo { get; set; }
        public bool EmpleadoPasivo { get; set; }
        public string Descripcion { get; set; }
        public bool Automatico { get; set; }
        public bool Reportado { get; set; }
        
        public int IdPeriodo { get; set; }
        public virtual PeriodoNomina PeriodoNomina { get; set; }

        public int IdProceso { get; set; }
        public virtual ProcesoNomina ProcesoNomina { get; set; }

        public virtual ICollection<ReportadoNomina> ReportadoNomina { get; set; }
        public virtual ICollection<HorasExtrasNomina> HorasExtrasNomina { get; set; }
        public virtual ICollection<CabeceraNomina> CabeceraNomina { get; set; }
        public virtual ICollection<DiasLaboradosNomina> DiasLaboradosNomina { get; set; }

    }
}
