using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class Sucursal
    {
        public Sucursal()
        {
            LibroActivoFijo = new HashSet<LibroActivoFijo>();
            MaestroArticuloSucursal = new HashSet<MaestroArticuloSucursal>();
        }

        public int IdSucursal { get; set; }
        public int IdCiudad { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<LibroActivoFijo> LibroActivoFijo { get; set; }
        public virtual ICollection<MaestroArticuloSucursal> MaestroArticuloSucursal { get; set; }
        public virtual Ciudad IdCiudadNavigation { get; set; }
    }
}
