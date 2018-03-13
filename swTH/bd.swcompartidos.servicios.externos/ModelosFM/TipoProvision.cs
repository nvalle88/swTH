using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class TipoProvision
    {
        public TipoProvision()
        {
            Provisiones = new HashSet<Provisiones>();
        }

        public int IdTipoProvision { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Provisiones> Provisiones { get; set; }
    }
}
