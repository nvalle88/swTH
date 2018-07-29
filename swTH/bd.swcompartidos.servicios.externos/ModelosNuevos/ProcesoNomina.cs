using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ProcesoNomina
    {
        public ProcesoNomina()
        {
            CalculoNomina = new HashSet<CalculoNomina>();
            ConceptoNomina = new HashSet<ConceptoNomina>();
        }

        public int IdProceso { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<CalculoNomina> CalculoNomina { get; set; }
        public virtual ICollection<ConceptoNomina> ConceptoNomina { get; set; }
    }
}
