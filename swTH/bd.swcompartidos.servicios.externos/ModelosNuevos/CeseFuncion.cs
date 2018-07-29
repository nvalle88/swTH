using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class CeseFuncion
    {
        public int IdCeseFuncion { get; set; }
        public int IdTipoCesacionFuncion { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public DateTime? FechaNotificacion { get; set; }

        public virtual TipoCesacionFuncion IdTipoCesacionFuncionNavigation { get; set; }
    }
}
