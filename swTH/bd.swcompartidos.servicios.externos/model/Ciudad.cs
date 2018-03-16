using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class Ciudad
    {
        public Ciudad()
        {
            ActivoFijo = new HashSet<ActivoFijo>();
            Empleado = new HashSet<Empleado>();
            Parroquia = new HashSet<Parroquia>();
            Sucursal = new HashSet<Sucursal>();
        }

        public int IdCiudad { get; set; }
        public int IdProvincia { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ActivoFijo> ActivoFijo { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<Parroquia> Parroquia { get; set; }
        public virtual ICollection<Sucursal> Sucursal { get; set; }
        public virtual Provincia IdProvinciaNavigation { get; set; }
    }
}
