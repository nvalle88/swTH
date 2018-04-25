using System;
using System.Collections.Generic;

namespace bd.swth.entidades.Negocio
{
    public partial class ConceptoConjuntoNomina
    {
        public int IdConceptoConjunto { get; set; }
        public int IdConjunto { get; set; }
        public int IdConcepto { get; set; }
        public bool? Suma { get; set; }
        public bool? Resta { get; set; }

        public virtual ConceptoNomina Concepto { get; set; }
        public virtual ConjuntoNomina Conjunto { get; set; }
    }
}
