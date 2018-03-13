using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class ItemViatico
    {
        public ItemViatico()
        {
            FacturaViatico = new HashSet<FacturaViatico>();
        }

        public int IdItemViatico { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<FacturaViatico> FacturaViatico { get; set; }
    }
}
