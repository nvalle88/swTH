using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class BrigadaSsorol
    {
        public BrigadaSsorol()
        {
            Empleado = new HashSet<Empleado>();
        }

        public int IdBrigadaSsorol { get; set; }
        public int IdBrigadaSso { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual BrigadaSso IdBrigadaSsoNavigation { get; set; }
    }
}
