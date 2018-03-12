using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class FormulasRmu
    {
        public FormulasRmu()
        {
            IngresoEgresoRmu = new HashSet<IngresoEgresoRmu>();
        }

        public int IdFormulaRmu { get; set; }
        public string Formula { get; set; }

        public virtual ICollection<IngresoEgresoRmu> IngresoEgresoRmu { get; set; }
    }
}
