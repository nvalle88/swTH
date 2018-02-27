using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class Parroquia
    {
        public Parroquia()
        {
            Persona = new HashSet<Persona>();
        }

        public int IdParroquia { get; set; }
        public int IdCiudad { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Persona> Persona { get; set; }
        public virtual Ciudad IdCiudadNavigation { get; set; }
    }
}
