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
        public string Descripcion { get; set; }

        public DateTime? FechaInicioDecimoTercero { get; set; }

        public DateTime? FechaFinDecimoTercero { get; set; }

        public DateTime? FechaInicioDecimoCuarto { get; set; }

        public DateTime? FechaFinDecimoCuarto { get; set; }

        public bool DecimoTercerSueldo { get; set; }
        public string DecimoCuartoSueldo { get; set; }
        
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
