using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class Sexo
    {
        public Sexo()
        {
            Persona = new HashSet<Persona>();
        }

        public int IdSexo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Persona> Persona { get; set; }
    }
}
