using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class BrigadaSso
    {
        public BrigadaSso()
        {
            BrigadaSsorol = new HashSet<BrigadaSsorol>();
        }

        public int IdBrigadaSso { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<BrigadaSsorol> BrigadaSsorol { get; set; }
    }
}
