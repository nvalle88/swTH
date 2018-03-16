using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class ActivosFijosBaja
    {
        public int IdBaja { get; set; }
        public int? IdMotivoBaja { get; set; }
        public DateTime? FechaBaja { get; set; }
        public int IdActivo { get; set; }

        public virtual ActivoFijo IdActivoNavigation { get; set; }
        public virtual ActivoFijoMotivoBaja IdMotivoBajaNavigation { get; set; }
    }
}
