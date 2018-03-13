using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class TipoTransporte
    {
        public TipoTransporte()
        {
            ItinerarioViatico = new HashSet<ItinerarioViatico>();
        }

        public int IdTipoTransporte { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<ItinerarioViatico> ItinerarioViatico { get; set; }
    }
}
