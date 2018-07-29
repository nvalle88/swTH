using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Ramo
    {
        public Ramo()
        {
            Subramo = new HashSet<Subramo>();
        }

        public int IdRamo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Subramo> Subramo { get; set; }
    }
}
