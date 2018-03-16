using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class TipoExamenComplementario
    {
        public TipoExamenComplementario()
        {
            ExamenComplementario = new HashSet<ExamenComplementario>();
        }

        public int IdTipoExamenComplementario { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ExamenComplementario> ExamenComplementario { get; set; }
    }
}
