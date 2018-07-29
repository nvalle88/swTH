using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class PolizaSeguroActivoFijo
    {
        public int IdActivo { get; set; }
        public int IdSubramo { get; set; }
        public int IdCompaniaSeguro { get; set; }
        public string NumeroPoliza { get; set; }

        public virtual ActivoFijo IdActivoNavigation { get; set; }
        public virtual CompaniaSeguro IdCompaniaSeguroNavigation { get; set; }
        public virtual Subramo IdSubramoNavigation { get; set; }
    }
}
