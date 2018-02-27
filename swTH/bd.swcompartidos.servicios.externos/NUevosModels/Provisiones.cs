using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class Provisiones
    {
        public int IdProvisiones { get; set; }
        public int IdTipoProvision { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Valor { get; set; }

        public virtual TipoProvision IdTipoProvisionNavigation { get; set; }
    }
}
