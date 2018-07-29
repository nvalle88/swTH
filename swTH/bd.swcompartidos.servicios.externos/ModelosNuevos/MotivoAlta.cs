using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class MotivoAlta
    {
        public MotivoAlta()
        {
            AltaActivoFijo = new HashSet<AltaActivoFijo>();
            RecepcionActivoFijo = new HashSet<RecepcionActivoFijo>();
        }

        public int IdMotivoAlta { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<AltaActivoFijo> AltaActivoFijo { get; set; }
        public virtual ICollection<RecepcionActivoFijo> RecepcionActivoFijo { get; set; }
    }
}
