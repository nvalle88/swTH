using System;
using System.Collections.Generic;

namespace bd.swth.entidades.Negocio
{
    public partial class LavadoActivoEmpleado
    {
        public int IdLavadoActivoEmpleado { get; set; }
        public int IdLavadoActivoItem { get; set; }
        public int IdEmpleado { get; set; }
        public bool Valor { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Empleado Empleado { get; set; }
        public virtual LavadoActivoItem LavadoActivoItem { get; set; }
    }
}
