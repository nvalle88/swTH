using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class FondoFinanciamiento
    {
        public FondoFinanciamiento()
        {
            IndiceOcupacionalModalidadPartida = new HashSet<IndiceOcupacionalModalidadPartida>();
        }

        public int IdFondoFinanciamiento { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<IndiceOcupacionalModalidadPartida> IndiceOcupacionalModalidadPartida { get; set; }
    }
}
