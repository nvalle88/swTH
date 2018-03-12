using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class ConocimientosAdicionales
    {
        public ConocimientosAdicionales()
        {
            IndiceOcupacionalConocimientosAdicionales = new HashSet<IndiceOcupacionalConocimientosAdicionales>();
        }

        public int IdConocimientosAdicionales { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<IndiceOcupacionalConocimientosAdicionales> IndiceOcupacionalConocimientosAdicionales { get; set; }
    }
}
