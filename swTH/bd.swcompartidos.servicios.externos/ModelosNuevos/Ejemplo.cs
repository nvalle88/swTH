using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Ejemplo
    {
        public Ejemplo()
        {
            Ejemplo1 = new HashSet<Ejemplo1>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int? Edad { get; set; }

        public virtual ICollection<Ejemplo1> Ejemplo1 { get; set; }
    }
}
