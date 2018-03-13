using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class CapacitacionAreaConocimiento
    {
        public CapacitacionAreaConocimiento()
        {
            CapacitacionTemario = new HashSet<CapacitacionTemario>();
        }

        public int IdCapacitacionAreaConocimiento { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<CapacitacionTemario> CapacitacionTemario { get; set; }
    }
}
