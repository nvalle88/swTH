using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class Proceso
    {
        public Proceso()
        {
            Dependencia = new HashSet<Dependencia>();
        }

        public int IdProceso { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Dependencia> Dependencia { get; set; }
    }
}
