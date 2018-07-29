using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ActivoFijo
    {
        public ActivoFijo()
        {
            DocumentoActivoFijo = new HashSet<DocumentoActivoFijo>();
            RecepcionActivoFijoDetalle = new HashSet<RecepcionActivoFijoDetalle>();
        }

        public int IdActivoFijo { get; set; }
        public int IdSubClaseActivoFijo { get; set; }
        public int IdModelo { get; set; }
        public string Nombre { get; set; }
        public decimal ValorCompra { get; set; }
        public bool Depreciacion { get; set; }

        public virtual ICollection<DocumentoActivoFijo> DocumentoActivoFijo { get; set; }
        public virtual PolizaSeguroActivoFijo PolizaSeguroActivoFijo { get; set; }
        public virtual ICollection<RecepcionActivoFijoDetalle> RecepcionActivoFijoDetalle { get; set; }
        public virtual Modelo IdModeloNavigation { get; set; }
        public virtual SubClaseActivoFijo IdSubClaseActivoFijoNavigation { get; set; }
    }
}
