using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class TipoSangre
    {
        public TipoSangre()
        {
            Persona = new HashSet<Persona>();
        }

        public int IdTipoSangre { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Persona> Persona { get; set; }
    }
}
