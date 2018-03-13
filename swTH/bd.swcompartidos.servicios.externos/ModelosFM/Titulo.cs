using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class Titulo
    {
        public int IdTitulo { get; set; }
        public int? IdAreaConocimiento { get; set; }
        public int IdEstudio { get; set; }
        public string Nombre { get; set; }

        public virtual AreaConocimiento IdAreaConocimientoNavigation { get; set; }
        public virtual Estudio IdEstudioNavigation { get; set; }
    }
}
