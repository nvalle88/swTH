using System;
using System.Collections.Generic;

namespace bd.swth.entidades.Negocio
{
    public partial class ConceptoNomina
    {
        public ConceptoNomina()
        {
            ConceptoProcesoNomina = new HashSet<ConceptoProcesoNomina>();
            FormulaNomina = new HashSet<FormulaNomina>();
        }

        public int IdConcepto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string TipoConcepto { get; set; }
        public string Estatus { get; set; }

        public virtual ICollection<ConceptoProcesoNomina> ConceptoProcesoNomina { get; set; }
        public virtual ICollection<FormulaNomina> FormulaNomina { get; set; }
    }
}
