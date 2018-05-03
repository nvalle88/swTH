namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EvaluacionCompetenciasTecnicasPuesto
    {
        [Key]
        public int IdEvaluacionCompetenciasTecnicasPuesto { get; set; }
        public int? IdDestreza { get; set; }
        public int? IdRelevancia { get; set; }
        public int? IdNivelDesarrollo { get; set; }
        public int? IdEval001 { get; set; }

        public virtual Destreza Destreza { get; set; }
        public virtual Eval001 Eval001 { get; set; }
        public virtual NivelDesarrollo NivelDesarrollo { get; set; }
        public virtual Relevancia Relevancia { get; set; }
    }
}
