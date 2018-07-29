using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class TeconceptoNomina
    {
        public int IdTeconcepto { get; set; }
        public int IdConcepto { get; set; }

        public virtual ConceptoNomina IdConceptoNavigation { get; set; }
    }
}
