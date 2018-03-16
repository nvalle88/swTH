using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class CodigoActivoFijo
    {
        public CodigoActivoFijo()
        {
            ActivoFijo = new HashSet<ActivoFijo>();
        }

        public int IdCodigoActivoFijo { get; set; }
        public string Codigosecuencial { get; set; }
        public string CodigoBarras { get; set; }

        public virtual ICollection<ActivoFijo> ActivoFijo { get; set; }
    }
}
