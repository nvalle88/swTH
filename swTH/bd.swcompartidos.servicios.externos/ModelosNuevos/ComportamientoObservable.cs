using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ComportamientoObservable
    {
        public ComportamientoObservable()
        {
            EvaluacionCompetenciasTecnicasPuesto = new HashSet<EvaluacionCompetenciasTecnicasPuesto>();
            EvaluacionCompetenciasUniversales = new HashSet<EvaluacionCompetenciasUniversales>();
            EvaluacionTrabajoEquipoIniciativaLiderazgo = new HashSet<EvaluacionTrabajoEquipoIniciativaLiderazgo>();
            IndiceOcupacionalComportamientoObservable = new HashSet<IndiceOcupacionalComportamientoObservable>();
        }

        public int IdComportamientoObservable { get; set; }
        public string Descripcion { get; set; }
        public int IdNivel { get; set; }
        public int IdDenominacionCompetencia { get; set; }

        public virtual ICollection<EvaluacionCompetenciasTecnicasPuesto> EvaluacionCompetenciasTecnicasPuesto { get; set; }
        public virtual ICollection<EvaluacionCompetenciasUniversales> EvaluacionCompetenciasUniversales { get; set; }
        public virtual ICollection<EvaluacionTrabajoEquipoIniciativaLiderazgo> EvaluacionTrabajoEquipoIniciativaLiderazgo { get; set; }
        public virtual ICollection<IndiceOcupacionalComportamientoObservable> IndiceOcupacionalComportamientoObservable { get; set; }
        public virtual DenominacionCompetencia IdDenominacionCompetenciaNavigation { get; set; }
        public virtual Nivel IdNivelNavigation { get; set; }
    }
}
