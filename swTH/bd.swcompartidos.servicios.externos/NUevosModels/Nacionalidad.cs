using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class Nacionalidad
    {
        public Nacionalidad()
        {
            Persona = new HashSet<Persona>();
        }

        public int IdNacionalidad { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Persona> Persona { get; set; }
    }
}
