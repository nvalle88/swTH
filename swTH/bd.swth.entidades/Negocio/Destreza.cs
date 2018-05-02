namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Destreza
    {
        [Key]
        public int IdDestreza { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EvaluacionCompetenciasTecnicasPuesto> EvaluacionCompetenciasTecnicasPuesto { get; set; }
        public virtual ICollection<EvaluacionCompetenciasUniversales> EvaluacionCompetenciasUniversales { get; set; }
    }
}
