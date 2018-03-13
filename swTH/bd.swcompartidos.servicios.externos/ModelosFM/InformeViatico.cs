using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class InformeViatico
    {
        public int IdInformeViatico { get; set; }
        public string Descripcion { get; set; }
        public int IdItinerarioViatico { get; set; }

        public virtual ItinerarioViatico IdItinerarioViaticoNavigation { get; set; }
    }
}
