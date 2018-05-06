namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public partial class ComportamientoObservable
    {
        [Key]
        public int IdComportamientoObservable { get; set; }
        public string Descripcion { get; set; }
        public int IdNivel { get; set; }
        public int IdDenominacionCompetencia { get; set; }

        public virtual ICollection<EvaluacionCompetenciasTecnicasPuesto> EvaluacionCompetenciasTecnicasPuesto { get; set; }
        public virtual ICollection<EvaluacionCompetenciasUniversales> EvaluacionCompetenciasUniversales { get; set; }
        public virtual ICollection<EvaluacionTrabajoEquipoIniciativaLiderazgo> EvaluacionTrabajoEquipoIniciativaLiderazgo { get; set; }
        public virtual ICollection<IndiceOcupacionalComportamientoObservable> IndiceOcupacionalComportamientoObservable { get; set; }
        public virtual DenominacionCompetencia DenominacionCompetencia { get; set; }
        public virtual Nivel Nivel{ get; set; }
    }
}
