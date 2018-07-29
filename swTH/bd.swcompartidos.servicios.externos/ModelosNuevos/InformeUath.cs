using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class InformeUath
    {
        public int IdInformeUath { get; set; }
        public bool? Revisar { get; set; }
        public int IdManualPuestoOrigen { get; set; }
        public int IdManualPuestoDestino { get; set; }
        public int IdAdministracionTalentoHumano { get; set; }

        public virtual AdministracionTalentoHumano IdAdministracionTalentoHumanoNavigation { get; set; }
        public virtual ManualPuesto IdManualPuestoDestinoNavigation { get; set; }
        public virtual ManualPuesto IdManualPuestoOrigenNavigation { get; set; }
    }
}
