using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class DocumentosIngreso
    {
        public DocumentosIngreso()
        {
            DocumentosIngresoEmpleado = new HashSet<DocumentosIngresoEmpleado>();
        }

        public int IdDocumentosIngreso { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<DocumentosIngresoEmpleado> DocumentosIngresoEmpleado { get; set; }
    }
}
