using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class TipoConcurso
    {
        public TipoConcurso()
        {
            PartidasFase = new HashSet<PartidasFase>();
        }

        public int IdTipoConcurso { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<PartidasFase> PartidasFase { get; set; }
    }
}
