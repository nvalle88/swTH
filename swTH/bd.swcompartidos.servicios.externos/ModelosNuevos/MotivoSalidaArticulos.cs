using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class MotivoSalidaArticulos
    {
        public MotivoSalidaArticulos()
        {
            SalidaArticulos = new HashSet<SalidaArticulos>();
        }

        public int IdMotivoSalidaArticulos { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<SalidaArticulos> SalidaArticulos { get; set; }
    }
}
