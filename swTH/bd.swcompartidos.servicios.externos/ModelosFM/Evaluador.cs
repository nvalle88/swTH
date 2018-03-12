using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class Evaluador
    {
        public Evaluador()
        {
            Eval001 = new HashSet<Eval001>();
        }

        public int IdEvaluador { get; set; }
        public DateTime Ano { get; set; }
        public int IdDependencia { get; set; }
        public int IdEmpleado { get; set; }

        public virtual ICollection<Eval001> Eval001 { get; set; }
        public virtual Dependencia IdDependenciaNavigation { get; set; }
    }
}
