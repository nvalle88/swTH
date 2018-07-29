using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class SriDetalle
    {
        public int IdSriDetalle { get; set; }
        public double FraccionBasica { get; set; }
        public double ExcesoHasta { get; set; }
        public double ImpFranccionBasica { get; set; }
        public double PorcientoImpFraccExced { get; set; }
        public int IdSri { get; set; }

        public virtual SriNomina IdSriNavigation { get; set; }
    }
}
