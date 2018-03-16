using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class SubClaseActivoFijo
    {
        public SubClaseActivoFijo()
        {
            ActivoFijo = new HashSet<ActivoFijo>();
            RecepcionActivoFijo = new HashSet<RecepcionActivoFijo>();
        }

        public int IdSubClaseActivoFijo { get; set; }
        public int IdClaseActivoFijo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ActivoFijo> ActivoFijo { get; set; }
        public virtual ICollection<RecepcionActivoFijo> RecepcionActivoFijo { get; set; }
        public virtual ClaseActivoFijo IdClaseActivoFijoNavigation { get; set; }
    }
}
