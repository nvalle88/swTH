using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class ExamenComplementario
    {
        public int IdExamenComplementario { get; set; }
        public DateTime Fecha { get; set; }
        public string Resultado { get; set; }
        public int IdTipoExamenComplementario { get; set; }
        public int IdFichaMedica { get; set; }

        public virtual FichaMedica IdFichaMedicaNavigation { get; set; }
        public virtual TipoExamenComplementario IdTipoExamenComplementarioNavigation { get; set; }
    }
}
