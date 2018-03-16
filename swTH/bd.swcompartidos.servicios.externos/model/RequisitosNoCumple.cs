using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class RequisitosNoCumple
    {
        public int IdRequisitosNoCumple { get; set; }
        public string Detalle { get; set; }
        public int IdAdministracionTalentoHumano { get; set; }

        public virtual AdministracionTalentoHumano IdAdministracionTalentoHumanoNavigation { get; set; }
    }
}
