using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class DenominacionCompetencia
    {
        public DenominacionCompetencia()
        {
            ComportamientoObservable = new HashSet<ComportamientoObservable>();
        }

        public int IdDenominacionCompetencia { get; set; }
        public string Nombre { get; set; }
        public string Definicion { get; set; }
        public bool? CompetenciaTecnica { get; set; }

        public virtual ICollection<ComportamientoObservable> ComportamientoObservable { get; set; }
    }
}
