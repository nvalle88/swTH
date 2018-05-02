namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EvaluacionConocimiento
    {
        [Key]
        public int IdEvaluacionConocimiento { get; set; }
        public int? IdNivelConocimiento { get; set; }

        public virtual ICollection<Eval001> Eval001 { get; set; }
        public virtual NivelConocimiento NivelConocimiento { get; set; }
    }
}
