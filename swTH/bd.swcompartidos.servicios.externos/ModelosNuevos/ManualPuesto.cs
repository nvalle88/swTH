using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ManualPuesto
    {
        public ManualPuesto()
        {
            FlujoAprobacion = new HashSet<FlujoAprobacion>();
            IndiceOcupacional = new HashSet<IndiceOcupacional>();
            InformeUathIdManualPuestoDestinoNavigation = new HashSet<InformeUath>();
            InformeUathIdManualPuestoOrigenNavigation = new HashSet<InformeUath>();
        }

        public int IdManualPuesto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Mision { get; set; }
        public int? IdRelacionesInternasExternas { get; set; }

        public virtual ICollection<FlujoAprobacion> FlujoAprobacion { get; set; }
        public virtual ICollection<IndiceOcupacional> IndiceOcupacional { get; set; }
        public virtual ICollection<InformeUath> InformeUathIdManualPuestoDestinoNavigation { get; set; }
        public virtual ICollection<InformeUath> InformeUathIdManualPuestoOrigenNavigation { get; set; }
        public virtual RelacionesInternasExternas IdRelacionesInternasExternasNavigation { get; set; }
    }
}
