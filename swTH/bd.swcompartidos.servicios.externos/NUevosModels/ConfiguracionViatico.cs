using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class ConfiguracionViatico
    {
        public int IdConfiguracionViatico { get; set; }
        public int IdRolPuesto { get; set; }
        public decimal? ValorEntregadoPorDia { get; set; }
        public string PorCientoAjustificar { get; set; }

        public virtual RolPuesto IdRolPuestoNavigation { get; set; }
    }
}
