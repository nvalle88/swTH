using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class AltaActivoFijo
    {
        public AltaActivoFijo()
        {
            AltaActivoFijoDetalle = new HashSet<AltaActivoFijoDetalle>();
            DocumentoActivoFijo = new HashSet<DocumentoActivoFijo>();
        }

        public int IdAltaActivoFijo { get; set; }
        public DateTime FechaAlta { get; set; }
        public int IdMotivoAlta { get; set; }
        public int? IdFacturaActivoFijo { get; set; }

        public virtual ICollection<AltaActivoFijoDetalle> AltaActivoFijoDetalle { get; set; }
        public virtual ICollection<DocumentoActivoFijo> DocumentoActivoFijo { get; set; }
        public virtual FacturaActivoFijo IdFacturaActivoFijoNavigation { get; set; }
        public virtual MotivoAlta IdMotivoAltaNavigation { get; set; }
    }
}
