using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class TransferenciaArticulo
    {
        public int IdTransferenciaArticulo { get; set; }
        public int IdMaestroArticuloEnvia { get; set; }
        public int IdArticulo { get; set; }
        public int IdEmpleadoEnvia { get; set; }
        public int IdMaestroRecibe { get; set; }
        public int IdempleadoRecibe { get; set; }
        public int? Cantidad { get; set; }
        public string Fecha { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdMaestroArticuloSucursal { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; }
        public virtual MaestroArticuloSucursal IdMaestroArticuloEnviaNavigation { get; set; }
        public virtual MaestroArticuloSucursal IdMaestroRecibeNavigation { get; set; }
    }
}
