using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class RubroLiquidacion
    {
        public RubroLiquidacion()
        {
            Liquidacion = new HashSet<Liquidacion>();
        }

        public int IdRubroLiquidacion { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Liquidacion> Liquidacion { get; set; }
    }
}
