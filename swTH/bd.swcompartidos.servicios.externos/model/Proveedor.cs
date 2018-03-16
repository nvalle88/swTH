using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Factura = new HashSet<Factura>();
            RecepcionActivoFijo = new HashSet<RecepcionActivoFijo>();
            RecepcionArticulos = new HashSet<RecepcionArticulos>();
        }

        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }

        public virtual ICollection<Factura> Factura { get; set; }
        public virtual ICollection<RecepcionActivoFijo> RecepcionActivoFijo { get; set; }
        public virtual ICollection<RecepcionArticulos> RecepcionArticulos { get; set; }
    }
}
