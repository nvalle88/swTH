using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class MaestroArticuloSucursal
    {
        public MaestroArticuloSucursal()
        {
            Factura = new HashSet<Factura>();
            MaestroDetalleArticulo = new HashSet<MaestroDetalleArticulo>();
            RecepcionArticulos = new HashSet<RecepcionArticulos>();
            TransferenciaArticuloIdMaestroArticuloEnviaNavigation = new HashSet<TransferenciaArticulo>();
            TransferenciaArticuloIdMaestroRecibeNavigation = new HashSet<TransferenciaArticulo>();
        }

        public int IdMaestroArticuloSucursal { get; set; }
        public int IdSucursal { get; set; }
        public int Minimo { get; set; }
        public int Maximo { get; set; }

        public virtual ICollection<Factura> Factura { get; set; }
        public virtual ICollection<MaestroDetalleArticulo> MaestroDetalleArticulo { get; set; }
        public virtual ICollection<RecepcionArticulos> RecepcionArticulos { get; set; }
        public virtual ICollection<TransferenciaArticulo> TransferenciaArticuloIdMaestroArticuloEnviaNavigation { get; set; }
        public virtual ICollection<TransferenciaArticulo> TransferenciaArticuloIdMaestroRecibeNavigation { get; set; }
        public virtual Sucursal IdSucursalNavigation { get; set; }
    }
}
