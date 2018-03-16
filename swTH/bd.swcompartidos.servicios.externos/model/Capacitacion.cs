using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class Capacitacion
    {
        public Capacitacion()
        {
            IndiceOcupacionalCapacitaciones = new HashSet<IndiceOcupacionalCapacitaciones>();
            PersonaCapacitacion = new HashSet<PersonaCapacitacion>();
        }

        public int IdCapacitacion { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<IndiceOcupacionalCapacitaciones> IndiceOcupacionalCapacitaciones { get; set; }
        public virtual ICollection<PersonaCapacitacion> PersonaCapacitacion { get; set; }
    }
}
