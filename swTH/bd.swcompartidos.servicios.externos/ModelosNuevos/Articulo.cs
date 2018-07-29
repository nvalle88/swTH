using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Articulo
    {
        public Articulo()
        {
            InventarioArticulos = new HashSet<InventarioArticulos>();
            MaestroArticuloSucursal = new HashSet<MaestroArticuloSucursal>();
        }

        public int IdArticulo { get; set; }
        public int IdSubClaseArticulo { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdModelo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<InventarioArticulos> InventarioArticulos { get; set; }
        public virtual ICollection<MaestroArticuloSucursal> MaestroArticuloSucursal { get; set; }
        public virtual Modelo IdModeloNavigation { get; set; }
        public virtual SubClaseArticulo IdSubClaseArticuloNavigation { get; set; }
        public virtual UnidadMedida IdUnidadMedidaNavigation { get; set; }
    }
}
