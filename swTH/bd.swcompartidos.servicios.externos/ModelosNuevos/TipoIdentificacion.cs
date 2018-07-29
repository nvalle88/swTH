using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class TipoIdentificacion
    {
        public TipoIdentificacion()
        {
            Persona = new HashSet<Persona>();
        }

        public int IdTipoIdentificacion { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Persona> Persona { get; set; }
    }
}
