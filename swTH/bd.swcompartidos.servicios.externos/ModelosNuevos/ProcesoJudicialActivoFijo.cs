using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ProcesoJudicialActivoFijo
    {
        public ProcesoJudicialActivoFijo()
        {
            DocumentoActivoFijo = new HashSet<DocumentoActivoFijo>();
        }

        public int IdProcesoJudicialActivoFijo { get; set; }
        public string NumeroDenuncia { get; set; }
        public int IdRecepcionActivoFijoDetalle { get; set; }

        public virtual ICollection<DocumentoActivoFijo> DocumentoActivoFijo { get; set; }
        public virtual RecepcionActivoFijoDetalle IdRecepcionActivoFijoDetalleNavigation { get; set; }
    }
}
