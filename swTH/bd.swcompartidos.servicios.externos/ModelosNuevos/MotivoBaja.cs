using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class MotivoBaja
    {
        public MotivoBaja()
        {
            BajaActivoFijo = new HashSet<BajaActivoFijo>();
        }

        public int IdMotivoBaja { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<BajaActivoFijo> BajaActivoFijo { get; set; }
    }
}
