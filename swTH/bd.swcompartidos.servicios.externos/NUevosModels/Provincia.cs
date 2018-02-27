using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class Provincia
    {
        public Provincia()
        {
            Ciudad = new HashSet<Ciudad>();
            Empleado = new HashSet<Empleado>();
            SolicitudViatico = new HashSet<SolicitudViatico>();
        }

        public int IdProvincia { get; set; }
        public int IdPais { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Ciudad> Ciudad { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<SolicitudViatico> SolicitudViatico { get; set; }
        public virtual Pais IdPaisNavigation { get; set; }
    }
}
