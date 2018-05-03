namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EvaluacionConocimiento
    {
        [Key]
        public int IdEvaluacionConocimiento { get; set; }
        public int? IdNivelConocimiento { get; set; }
        public int? IdEval001 { get; set; }

        public virtual Eval001 Eval001 { get; set; }
        public virtual NivelConocimiento NivelConocimiento { get; set; }
    }
}
