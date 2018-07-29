using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Quejas
    {
        public int IdQueja { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Descripcion { get; set; }
        public string NumeroFormulario { get; set; }
        public bool? AplicaDescuento { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdEval001 { get; set; }

        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual Eval001 IdEval001Navigation { get; set; }
    }
}
