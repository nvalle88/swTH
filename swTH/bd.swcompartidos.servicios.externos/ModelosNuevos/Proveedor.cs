using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            OrdenCompra = new HashSet<OrdenCompra>();
            RecepcionActivoFijo = new HashSet<RecepcionActivoFijo>();
            SalidaArticulos = new HashSet<SalidaArticulos>();
        }

        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Codigo { get; set; }
        public int IdLineaServicio { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<OrdenCompra> OrdenCompra { get; set; }
        public virtual ICollection<RecepcionActivoFijo> RecepcionActivoFijo { get; set; }
        public virtual ICollection<SalidaArticulos> SalidaArticulos { get; set; }
        public virtual LineaServicio IdLineaServicioNavigation { get; set; }
    }
}
