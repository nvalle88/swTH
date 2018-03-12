using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class InformeUath
    {
        public int IdInformeUath { get; set; }
        public bool? Revisar { get; set; }
        public int ManualPuestoOrigen { get; set; }
        public int ManualPuestoDestino { get; set; }
        public int IdAdministracionTalentoHumano { get; set; }

        public virtual AdministracionTalentoHumano IdAdministracionTalentoHumanoNavigation { get; set; }
        public virtual ManualPuesto ManualPuestoDestinoNavigation { get; set; }
        public virtual ManualPuesto ManualPuestoOrigenNavigation { get; set; }
    }
}
