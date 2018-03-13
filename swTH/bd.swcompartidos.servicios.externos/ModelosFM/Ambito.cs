using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class Ambito
    {
        public Ambito()
        {
            IndiceOcupacional = new HashSet<IndiceOcupacional>();
        }

        public int IdAmbito { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<IndiceOcupacional> IndiceOcupacional { get; set; }
    }
}
