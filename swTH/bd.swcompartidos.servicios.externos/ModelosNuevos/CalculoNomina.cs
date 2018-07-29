using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class CalculoNomina
    {
        public CalculoNomina()
        {
            CabeceraNomina = new HashSet<CabeceraNomina>();
            HorasExtrasNomina = new HashSet<HorasExtrasNomina>();
            ReportadoNomina = new HashSet<ReportadoNomina>();
        }

        public int IdCalculoNomina { get; set; }
        public bool? Automatico { get; set; }
        public bool? Reportado { get; set; }
        public bool? EmpleadoActivo { get; set; }
        public bool? EmpleadoPasivo { get; set; }
        public int? IdPeriodo { get; set; }
        public int? IdProceso { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<CabeceraNomina> CabeceraNomina { get; set; }
        public virtual ICollection<HorasExtrasNomina> HorasExtrasNomina { get; set; }
        public virtual ICollection<ReportadoNomina> ReportadoNomina { get; set; }
        public virtual PeriodoNomina IdPeriodoNavigation { get; set; }
        public virtual ProcesoNomina IdProcesoNavigation { get; set; }
    }
}
