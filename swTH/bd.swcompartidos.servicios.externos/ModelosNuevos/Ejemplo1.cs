using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Ejemplo1
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdEjemplo { get; set; }

        public virtual Ejemplo IdEjemploNavigation { get; set; }
    }
}
