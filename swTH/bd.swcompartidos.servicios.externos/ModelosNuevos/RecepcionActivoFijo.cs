using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class RecepcionActivoFijo
    {
        public RecepcionActivoFijo()
        {
            RecepcionActivoFijoDetalle = new HashSet<RecepcionActivoFijoDetalle>();
        }

        public int IdRecepcionActivoFijo { get; set; }
        public int IdMotivoAlta { get; set; }
        public int IdProveedor { get; set; }
        public int IdFondoFinanciamiento { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public int Cantidad { get; set; }
        public bool ValidacionTecnica { get; set; }
        public string OrdenCompra { get; set; }

        public virtual ICollection<RecepcionActivoFijoDetalle> RecepcionActivoFijoDetalle { get; set; }
        public virtual FondoFinanciamiento IdFondoFinanciamientoNavigation { get; set; }
        public virtual MotivoAlta IdMotivoAltaNavigation { get; set; }
        public virtual Proveedor IdProveedorNavigation { get; set; }
    }
}
