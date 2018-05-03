namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EvaluacionActividadesPuestoTrabajo
    {
        [Key]
        public int IdEvaluacionActividadesPuestoTrabajo { get; set; }
        public int? IdIndicador { get; set; }
        public string DescripcionActividad { get; set; }
        public int MetaPeriodo { get; set; }
        public int ActividadesCumplidas { get; set; }
        public int IdActividadesEsenciales { get; set; }
        public int? IdEval001 { get; set; }
        public int? Aumento { get; set; }

        public virtual ActividadesEsenciales ActividadesEsenciales { get; set; }
        public virtual Eval001 Eval001 { get; set; }
        public virtual Indicador Indicador { get; set; }
    }
}
