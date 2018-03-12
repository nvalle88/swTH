using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class AccionPersonal
    {
        public int IdAccionPersonal { get; set; }
        public int IdEmpleado { get; set; }
        public int IdTipoAccionPersonal { get; set; }
        public DateTime? Fecha { get; set; }
        public string Numero { get; set; }
        public string Solicitud { get; set; }
        public string Explicacion { get; set; }
        public DateTime? FechaRige { get; set; }
        public DateTime? FechaRigeHasta { get; set; }
        public int? NoDias { get; set; }
        public int Estado { get; set; }

        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual TipoAccionPersonal IdTipoAccionPersonalNavigation { get; set; }
    }
}
