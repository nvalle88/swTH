using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class DocumentosIngresoEmpleado
    {
        public int IdDocumentosIngresoEmpleado { get; set; }
        public int IdEmpleado { get; set; }
        public int IdDocumentosIngreso { get; set; }
        public bool? Entregado { get; set; }

        public virtual DocumentosIngreso IdDocumentosIngresoNavigation { get; set; }
    }
}
