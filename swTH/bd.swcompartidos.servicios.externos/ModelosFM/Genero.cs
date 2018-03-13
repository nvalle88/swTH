using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class Genero
    {
        public Genero()
        {
            Persona = new HashSet<Persona>();
        }

        public int IdGenero { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Persona> Persona { get; set; }
    }
}
