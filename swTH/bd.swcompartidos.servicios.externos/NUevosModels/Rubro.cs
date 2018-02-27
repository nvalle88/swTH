using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class Rubro
    {
        public Rubro()
        {
            GastoRubro = new HashSet<GastoRubro>();
        }

        public int IdRubro { get; set; }
        public string Nombre { get; set; }
        public decimal? TasaPorcentualMaxima { get; set; }

        public virtual ICollection<GastoRubro> GastoRubro { get; set; }
    }
}
