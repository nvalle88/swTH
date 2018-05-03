using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.Negocio
{
   public class SriDetalle
    {
        [Key]
        public int IdSriDetalle { get; set; }
        public double FraccionBasica { get; set; }
        public double ExcesoHasta { get; set; }
        public double ImpFranccionBasica { get; set; }
        public double PorcientoImpFraccExced { get; set; }

        public int IdSri { get; set; }
        public virtual SriNomina SriNomina { get; set; }
    }
}
