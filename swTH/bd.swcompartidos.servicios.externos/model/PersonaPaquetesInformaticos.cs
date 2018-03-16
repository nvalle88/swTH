using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class PersonaPaquetesInformaticos
    {
        public int IdPersonaPaquetesInformaticos { get; set; }
        public int IdPaquetesInformaticos { get; set; }
        public int IdEmpleado { get; set; }

        public virtual PaquetesInformaticos IdPaquetesInformaticosNavigation { get; set; }
    }
}
