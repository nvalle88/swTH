using System;
using System.Collections.Generic;

namespace bd.swth.entidades.Negocio
{
    public partial class LavadoActivoItem
    {
        public LavadoActivoItem()
        {
            LavadoActivoEmpleado = new HashSet<LavadoActivoEmpleado>();
        }

        public int IdLavadoActivoItem { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<LavadoActivoEmpleado> LavadoActivoEmpleado { get; set; }
    }
}
