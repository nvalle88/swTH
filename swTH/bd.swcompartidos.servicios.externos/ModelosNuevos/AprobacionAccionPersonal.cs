using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class AprobacionAccionPersonal
    {
        public int IdAprobacionAccionPersonal { get; set; }
        public int IdAccionPersonal { get; set; }
        public int IdFlujoAprobacion { get; set; }
        public int IdEmpleadoAprobador { get; set; }
        public int Estado { get; set; }
        public DateTime FechaAprobacion { get; set; }

        public virtual AccionPersonal IdAccionPersonalNavigation { get; set; }
        public virtual Empleado IdEmpleadoAprobadorNavigation { get; set; }
        public virtual FlujoAprobacion IdFlujoAprobacionNavigation { get; set; }
    }
}
