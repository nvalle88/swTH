using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ConfiguracionViatico
    {
        public ConfiguracionViatico()
        {
            SolicitudViatico = new HashSet<SolicitudViatico>();
        }

        public int IdConfiguracionViatico { get; set; }
        public int IdRolPuesto { get; set; }
        public decimal? ValorEntregadoPorDia { get; set; }
        public string PorCientoAjustificar { get; set; }

        public virtual ICollection<SolicitudViatico> SolicitudViatico { get; set; }
        public virtual RolPuesto IdRolPuestoNavigation { get; set; }
    }
}
