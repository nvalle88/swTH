using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class ManualPuesto
    {
        public ManualPuesto()
        {
            IndiceOcupacional = new HashSet<IndiceOcupacional>();
            InformeUathManualPuestoDestinoNavigation = new HashSet<InformeUath>();
            InformeUathManualPuestoOrigenNavigation = new HashSet<InformeUath>();
        }

        public int IdManualPuesto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Mision { get; set; }
        public int? IdRelacionesInternasExternas { get; set; }

        public virtual ICollection<IndiceOcupacional> IndiceOcupacional { get; set; }
        public virtual ICollection<InformeUath> InformeUathManualPuestoDestinoNavigation { get; set; }
        public virtual ICollection<InformeUath> InformeUathManualPuestoOrigenNavigation { get; set; }
        public virtual RelacionesInternasExternas IdRelacionesInternasExternasNavigation { get; set; }
    }
}
