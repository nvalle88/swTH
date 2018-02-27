using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class Parentesco
    {
        public Parentesco()
        {
            EmpleadoContactoEmergencia = new HashSet<EmpleadoContactoEmergencia>();
            EmpleadoFamiliar = new HashSet<EmpleadoFamiliar>();
            PersonaSustituto = new HashSet<PersonaSustituto>();
        }

        public int IdParentesco { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EmpleadoContactoEmergencia> EmpleadoContactoEmergencia { get; set; }
        public virtual ICollection<EmpleadoFamiliar> EmpleadoFamiliar { get; set; }
        public virtual ICollection<PersonaSustituto> PersonaSustituto { get; set; }
    }
}
