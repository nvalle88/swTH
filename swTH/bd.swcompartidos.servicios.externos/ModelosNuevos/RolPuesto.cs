using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class RolPuesto
    {
        public RolPuesto()
        {
            AdministracionTalentoHumano = new HashSet<AdministracionTalentoHumano>();
            ConfiguracionViatico = new HashSet<ConfiguracionViatico>();
            IndiceOcupacional = new HashSet<IndiceOcupacional>();
            SituacionPropuesta = new HashSet<SituacionPropuesta>();
        }

        public int IdRolPuesto { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<AdministracionTalentoHumano> AdministracionTalentoHumano { get; set; }
        public virtual ICollection<ConfiguracionViatico> ConfiguracionViatico { get; set; }
        public virtual ICollection<IndiceOcupacional> IndiceOcupacional { get; set; }
        public virtual ICollection<SituacionPropuesta> SituacionPropuesta { get; set; }
    }
}
