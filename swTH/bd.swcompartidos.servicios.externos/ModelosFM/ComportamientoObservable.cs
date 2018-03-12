using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class ComportamientoObservable
    {
        public ComportamientoObservable()
        {
            IndiceOcupacionalComportamientoObservable = new HashSet<IndiceOcupacionalComportamientoObservable>();
        }

        public int IdComportamientoObservable { get; set; }
        public string Descripcion { get; set; }
        public int IdNivel { get; set; }
        public int IdDenominacionCompetencia { get; set; }

        public virtual ICollection<IndiceOcupacionalComportamientoObservable> IndiceOcupacionalComportamientoObservable { get; set; }
        public virtual DenominacionCompetencia IdDenominacionCompetenciaNavigation { get; set; }
        public virtual Nivel IdNivelNavigation { get; set; }
    }
}
