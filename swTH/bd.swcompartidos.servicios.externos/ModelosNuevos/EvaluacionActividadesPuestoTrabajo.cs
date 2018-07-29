using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class EvaluacionActividadesPuestoTrabajo
    {
        public int IdEvaluacionActividadesPuestoTrabajo { get; set; }
        public int? IdIndicador { get; set; }
        public string DescripcionActividad { get; set; }
        public int MetaPeriodo { get; set; }
        public int ActividadesCumplidas { get; set; }
        public int IdActividadesEsenciales { get; set; }
        public int? IdEval001 { get; set; }
        public double? PorcetajeCumplimiento { get; set; }
        public int? NivelCumplimiento { get; set; }
        public int? Aumento { get; set; }

        public virtual ActividadesEsenciales IdActividadesEsencialesNavigation { get; set; }
        public virtual Eval001 IdEval001Navigation { get; set; }
        public virtual Indicador IdIndicadorNavigation { get; set; }
    }
}
