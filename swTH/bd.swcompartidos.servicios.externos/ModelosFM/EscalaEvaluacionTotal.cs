using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class EscalaEvaluacionTotal
    {
        public EscalaEvaluacionTotal()
        {
            Eval001 = new HashSet<Eval001>();
        }

        public int IdEscalaEvaluacionTotal { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public decimal PorcientoDesde { get; set; }
        public decimal PorcientoHasta { get; set; }

        public virtual ICollection<Eval001> Eval001 { get; set; }
    }
}
