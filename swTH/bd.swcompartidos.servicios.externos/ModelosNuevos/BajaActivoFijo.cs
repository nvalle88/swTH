using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class BajaActivoFijo
    {
        public BajaActivoFijo()
        {
            BajaActivoFijoDetalle = new HashSet<BajaActivoFijoDetalle>();
        }

        public int IdBajaActivoFijo { get; set; }
        public int IdMotivoBaja { get; set; }
        public DateTime FechaBaja { get; set; }
        public string MemoOficioResolucion { get; set; }

        public virtual ICollection<BajaActivoFijoDetalle> BajaActivoFijoDetalle { get; set; }
        public virtual MotivoBaja IdMotivoBajaNavigation { get; set; }
    }
}
