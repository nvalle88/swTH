using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class FacturaActivoFijo
    {
        public FacturaActivoFijo()
        {
            AltaActivoFijo = new HashSet<AltaActivoFijo>();
            DocumentoActivoFijo = new HashSet<DocumentoActivoFijo>();
            OrdenCompra = new HashSet<OrdenCompra>();
        }

        public int IdFacturaActivoFijo { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime FechaFactura { get; set; }

        public virtual ICollection<AltaActivoFijo> AltaActivoFijo { get; set; }
        public virtual ICollection<DocumentoActivoFijo> DocumentoActivoFijo { get; set; }
        public virtual ICollection<OrdenCompra> OrdenCompra { get; set; }
    }
}
