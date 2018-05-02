namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EvaluacionCompetenciasUniversales
    {
        [Key]
        public int IdEvaluacionCompetenciasUniversales { get; set; }
        public int? IdDestreza { get; set; }
        public int? IdRelevancia { get; set; }
        public int? IdFrecuenciaAplicacion { get; set; }

        public virtual ICollection<Eval001> Eval001 { get; set; }
        public virtual Destreza Destreza { get; set; }
        public virtual FrecuenciaAplicacion FrecuenciaAplicacion { get; set; }
        public virtual Relevancia Relevancia { get; set; }
    }
}
