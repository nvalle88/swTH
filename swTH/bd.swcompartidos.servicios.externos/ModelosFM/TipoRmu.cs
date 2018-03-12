using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class TipoRmu
    {
        public TipoRmu()
        {
            Rmu = new HashSet<Rmu>();
        }

        public int IdTipoRmu { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Rmu> Rmu { get; set; }
    }
}
