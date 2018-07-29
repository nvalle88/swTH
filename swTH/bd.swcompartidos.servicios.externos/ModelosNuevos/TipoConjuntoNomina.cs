using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class TipoConjuntoNomina
    {
        public TipoConjuntoNomina()
        {
            ConjuntoNomina = new HashSet<ConjuntoNomina>();
        }

        public int IdTipoConjunto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<ConjuntoNomina> ConjuntoNomina { get; set; }
    }
}
