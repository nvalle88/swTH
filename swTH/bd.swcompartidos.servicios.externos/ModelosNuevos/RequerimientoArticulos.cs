using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class RequerimientoArticulos
    {
        public RequerimientoArticulos()
        {
            RequerimientosArticulosDetalles = new HashSet<RequerimientosArticulosDetalles>();
            SalidaArticulos = new HashSet<SalidaArticulos>();
        }

        public int IdRequerimientoArticulos { get; set; }
        public int IdFuncionarioSolicitante { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaAprobadoDenegado { get; set; }
        public int IdEstado { get; set; }
        public string Observaciones { get; set; }

        public virtual ICollection<RequerimientosArticulosDetalles> RequerimientosArticulosDetalles { get; set; }
        public virtual ICollection<SalidaArticulos> SalidaArticulos { get; set; }
        public virtual Estado IdEstadoNavigation { get; set; }
        public virtual Empleado IdFuncionarioSolicitanteNavigation { get; set; }
    }
}
