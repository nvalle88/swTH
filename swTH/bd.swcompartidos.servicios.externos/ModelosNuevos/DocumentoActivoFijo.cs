using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class DocumentoActivoFijo
    {
        public int IdDocumentoActivoFijo { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Url { get; set; }
        public int? IdActivoFijo { get; set; }
        public int? IdRecepcionActivoFijoDetalle { get; set; }
        public int? IdAltaActivoFijo { get; set; }
        public int? IdFacturaActivoFijo { get; set; }
        public int? IdProcesoJudicialActivoFijo { get; set; }

        public virtual ActivoFijo IdActivoFijoNavigation { get; set; }
        public virtual AltaActivoFijo IdAltaActivoFijoNavigation { get; set; }
        public virtual FacturaActivoFijo IdFacturaActivoFijoNavigation { get; set; }
        public virtual ProcesoJudicialActivoFijo IdProcesoJudicialActivoFijoNavigation { get; set; }
        public virtual RecepcionActivoFijoDetalle IdRecepcionActivoFijoDetalleNavigation { get; set; }
    }
}
