using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class RolPagoDetalle
    {
        public int IdRolPagoDetalle { get; set; }
        public int IdRolPagos { get; set; }
        public string Descripcion { get; set; }
        public decimal Valor { get; set; }
        public bool EsIngreso { get; set; }

        public virtual RolPagos IdRolPagosNavigation { get; set; }
    }
}
