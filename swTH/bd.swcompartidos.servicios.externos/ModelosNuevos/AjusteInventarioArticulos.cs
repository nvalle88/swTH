using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class AjusteInventarioArticulos
    {
        public int IdAjusteInventario { get; set; }
        public string Motivo { get; set; }
        public int IdEmpleadoAutoriza { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Empleado IdEmpleadoAutorizaNavigation { get; set; }
    }
}
