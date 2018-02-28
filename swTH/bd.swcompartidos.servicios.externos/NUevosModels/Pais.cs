using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class Pais
    {
        public Pais()
        {
            CapacitacionProveedor = new HashSet<CapacitacionProveedor>();
            Provincia = new HashSet<Provincia>();
            SolicitudViatico = new HashSet<SolicitudViatico>();
        }

        public int IdPais { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<CapacitacionProveedor> CapacitacionProveedor { get; set; }
        public virtual ICollection<Provincia> Provincia { get; set; }
        public virtual ICollection<SolicitudViatico> SolicitudViatico { get; set; }
    }
}
