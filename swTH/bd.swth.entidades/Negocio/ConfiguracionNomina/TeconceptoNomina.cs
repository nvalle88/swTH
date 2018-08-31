using System;
using System.Collections.Generic;

namespace bd.swth.entidades.Negocio
{
    public partial class TeconceptoNomina
    {
        //Ejemplo
        public int IdTeconcepto { get; set; }
        public int IdConcepto { get; set; }

        public virtual ConceptoNomina ConceptoNomina { get; set; }
    }
}
