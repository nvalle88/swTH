using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class Articulo
    {
        public Articulo()
        {
            DetalleFactura = new HashSet<DetalleFactura>();
            MaestroDetalleArticulo = new HashSet<MaestroDetalleArticulo>();
            RecepcionArticulos = new HashSet<RecepcionArticulos>();
            TransferenciaArticulo = new HashSet<TransferenciaArticulo>();
        }

        public int IdArticulo { get; set; }
        public int IdSubClaseArticulo { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdModelo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<DetalleFactura> DetalleFactura { get; set; }
        public virtual ICollection<MaestroDetalleArticulo> MaestroDetalleArticulo { get; set; }
        public virtual ICollection<RecepcionArticulos> RecepcionArticulos { get; set; }
        public virtual ICollection<TransferenciaArticulo> TransferenciaArticulo { get; set; }
        public virtual Modelo IdModeloNavigation { get; set; }
        public virtual SubClaseArticulo IdSubClaseArticuloNavigation { get; set; }
        public virtual UnidadMedida IdUnidadMedidaNavigation { get; set; }
    }
}
