using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class ActivarPersonalTalentoHumano
    {
        public int IdActivarPersonalTalentoHumano { get; set; }
        public int IdDependencia { get; set; }
        public DateTime Fecha { get; set; }
        public int Estado { get; set; }

        public virtual Dependencia IdDependenciaNavigation { get; set; }
    }
}
