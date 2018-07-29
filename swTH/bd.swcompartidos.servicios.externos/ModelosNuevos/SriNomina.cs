using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class SriNomina
    {
        public SriNomina()
        {
            SriDetalle = new HashSet<SriDetalle>();
        }

        public int IdSri { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public bool Abierto { get; set; }

        public virtual ICollection<SriDetalle> SriDetalle { get; set; }
    }
}
