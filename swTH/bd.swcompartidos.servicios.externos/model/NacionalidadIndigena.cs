using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class NacionalidadIndigena
    {
        public NacionalidadIndigena()
        {
            Persona = new HashSet<Persona>();
        }

        public int IdNacionalidadIndigena { get; set; }
        public int IdEtnia { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Persona> Persona { get; set; }
        public virtual Etnia IdEtniaNavigation { get; set; }
    }
}
