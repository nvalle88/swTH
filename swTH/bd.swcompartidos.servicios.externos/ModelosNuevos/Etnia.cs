using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Etnia
    {
        public Etnia()
        {
            NacionalidadIndigena = new HashSet<NacionalidadIndigena>();
            Persona = new HashSet<Persona>();
        }

        public int IdEtnia { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<NacionalidadIndigena> NacionalidadIndigena { get; set; }
        public virtual ICollection<Persona> Persona { get; set; }
    }
}
