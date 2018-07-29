using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Sucursal
    {
        public Sucursal()
        {
            Bodega = new HashSet<Bodega>();
            FlujoAprobacion = new HashSet<FlujoAprobacion>();
            LibroActivoFijo = new HashSet<LibroActivoFijo>();
            MaestroArticuloSucursal = new HashSet<MaestroArticuloSucursal>();
            Presupuesto = new HashSet<Presupuesto>();
        }

        public int IdSucursal { get; set; }
        public int IdCiudad { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        public virtual ICollection<Bodega> Bodega { get; set; }
        public virtual ICollection<FlujoAprobacion> FlujoAprobacion { get; set; }
        public virtual ICollection<LibroActivoFijo> LibroActivoFijo { get; set; }
        public virtual ICollection<MaestroArticuloSucursal> MaestroArticuloSucursal { get; set; }
        public virtual ICollection<Presupuesto> Presupuesto { get; set; }
        public virtual Ciudad IdCiudadNavigation { get; set; }
    }
}
