using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class AntecedentesLaborales
    {
        public int IdAntecedentesLaborales { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public string TiempoTrabajo { get; set; }
        public string DetalleRiesgosExpuesto { get; set; }
        public string EppUtilizados { get; set; }
        public int IdFichaMedica { get; set; }

        public virtual FichaMedica IdFichaMedicaNavigation { get; set; }
    }
}
