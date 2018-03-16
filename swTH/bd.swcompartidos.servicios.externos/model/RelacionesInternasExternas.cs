using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class RelacionesInternasExternas
    {
        public RelacionesInternasExternas()
        {
            ManualPuesto = new HashSet<ManualPuesto>();
        }

        public int IdRelacionesInternasExternas { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<ManualPuesto> ManualPuesto { get; set; }
    }
}
