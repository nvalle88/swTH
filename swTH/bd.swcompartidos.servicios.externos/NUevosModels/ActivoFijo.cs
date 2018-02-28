using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class ActivoFijo
    {
        public ActivoFijo()
        {
            ActivosFijosBaja = new HashSet<ActivosFijosBaja>();
            DepreciacionActivoFijo = new HashSet<DepreciacionActivoFijo>();
            EmpleadoActivoFijo = new HashSet<EmpleadoActivoFijo>();
            MantenimientoActivoFijo = new HashSet<MantenimientoActivoFijo>();
            TransferenciaActivoFijoDetalle = new HashSet<TransferenciaActivoFijoDetalle>();
        }

        public int IdActivoFijo { get; set; }
        public int IdSubClaseActivoFijo { get; set; }
        public int IdLibroActivoFijo { get; set; }
        public int IdCiudad { get; set; }
        public int IdUnidadMedida { get; set; }
        public int IdCodigoActivoFijo { get; set; }
        public int IdModelo { get; set; }
        public string Nombre { get; set; }
        public string Serie { get; set; }
        public decimal ValorCompra { get; set; }
        public string Ubicacion { get; set; }

        public virtual ActivosFijosAlta ActivosFijosAlta { get; set; }
        public virtual ICollection<ActivosFijosBaja> ActivosFijosBaja { get; set; }
        public virtual ICollection<DepreciacionActivoFijo> DepreciacionActivoFijo { get; set; }
        public virtual ICollection<EmpleadoActivoFijo> EmpleadoActivoFijo { get; set; }
        public virtual ICollection<MantenimientoActivoFijo> MantenimientoActivoFijo { get; set; }
        public virtual ICollection<TransferenciaActivoFijoDetalle> TransferenciaActivoFijoDetalle { get; set; }
        public virtual Ciudad IdCiudadNavigation { get; set; }
        public virtual CodigoActivoFijo IdCodigoActivoFijoNavigation { get; set; }
        public virtual LibroActivoFijo IdLibroActivoFijoNavigation { get; set; }
        public virtual Modelo IdModeloNavigation { get; set; }
        public virtual SubClaseActivoFijo IdSubClaseActivoFijoNavigation { get; set; }
        public virtual UnidadMedida IdUnidadMedidaNavigation { get; set; }
    }
}
